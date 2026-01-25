using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChecker : MonoBehaviour
{
    public AppodealADSScript ads;
    public Text textScore;
    private List<GameObject> dices = new List<GameObject>();
    private string[] tags = new string[] { "D4", "D6", "D8", "D10", "D12", "D20" };
    private int totalScore = 0;

    // НОВАЯ ПЕРЕМЕННАЯ: Флаг, отслеживающий, ждем ли мы остановки
    private bool isRolling = false;
    private float stopTimer = 0f; // Таймер покоя
    private const float timeToWait = 0.8f; // Сколько секунд кубики должны лежать неподвижно

    void Update()
    {
        UpdateDiceList();
        bool allStopped = IsAllStopped();

        if (!allStopped)
        {
            // Если хоть один кубик движется — сбрасываем всё
            isRolling = true;
            stopTimer = 0f;
        }
        else if (isRolling && allStopped)
        {
            // Если кубики замерли, начинаем отсчет таймера
            stopTimer += Time.deltaTime;

            if (stopTimer >= timeToWait)
            {
                // Только по истечении времени засчитываем бросок
                scoreWrite();
                isRolling = false;
                stopTimer = 0f;
            }
        }
    }


    private void scoreWrite()
    {
        totalScore = 0;
        foreach (GameObject dice in dices)
        {
            totalScore += dice.GetComponent<SideChecker>().score;
        }

        textScore.text = totalScore.ToString();

        if (ads != null)
        {
            ads.OnDiceThrown(); // Теперь вызовется строго 1 раз после остановки
        }
    }

    // Оптимизация: ищем объекты реже или только когда нужно
    private void UpdateDiceList()
    {
        dices.Clear();
        foreach (string tag in tags)
        {
            GameObject[] found = GameObject.FindGameObjectsWithTag(tag);
            dices.AddRange(found);
        }
    }

    bool IsAllStopped()
    {
        if (dices.Count == 0) return true;

        foreach (GameObject dice in dices)
        {
            // Проверка на 0 скорости. 
            // linearVelocity.magnitude < 0.05f лучше, так как физика может "дрожать"
            if (dice.GetComponent<Rigidbody>().linearVelocity.magnitude > 0.05f)
            {
                return false; // Хотя бы один еще катится
            }
        }
        return true; // Все лежат
    }
}
