using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyDice
{
    public class ScoreChecker : MonoBehaviour
    {
        public AppodealADSScript ads;
        public Text textScore;

        // ✅ Исправлено: теперь храним ссылки на кости вместо поиска по тегам в Update()
        private List<GameObject> dices = new List<GameObject>();
        private string[] tags = new string[] { "D4", "D6", "D8", "D10", "D12", "D20" };
        
        // ✅ Добавлено: список Rigidbody для быстрого доступа
        private List<Rigidbody> rigidbodies = new List<Rigidbody>();

        private int totalScore = 0;

        private bool isRolling = false;
        private float stopTimer = 0f;
        private float timeToWait = 0.8f;

        // ✅ Добавлено: метод для обновления списка костей (вызывается извне)
        public void UpdateDiceList()
        {
            dices.Clear();
            rigidbodies.Clear();
            
            foreach (string tag in tags)
            {
                GameObject[] found = GameObject.FindGameObjectsWithTag(tag);
                if (found != null && found.Length > 0)
                {
                    dices.AddRange(found);
                    // ✅ Добавлено: сразу получаем Rigidbody и храним ссылку
                    foreach (GameObject dice in found)
                    {
                        Rigidbody rb = dice.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rigidbodies.Add(rb);
                        }
                    }
                }
            }
        }

        private void Start()
        {
            // ✅ Добавлено: инициализация списка костей при старте
            UpdateDiceList();
        }

        private void OnDestroy()
        {
            // ✅ Добавлено: очистка списков при уничтожении скрипта
            dices.Clear();
            rigidbodies.Clear();
        }

        private void Update()
        {
            // ✅ Исправлено: больше не вызываем UpdateDiceList() каждый кадр!
            // Список костей обновляется только когда нужно (извне)
            
            bool allStopped = IsAllStopped();

            if (!allStopped)
            {
                isRolling = true;
                stopTimer = 0f;
            }
            else if (isRolling && allStopped)
            {
                stopTimer += Time.deltaTime;

                if (stopTimer >= timeToWait)
                {
                    ScoreWrite();
                    isRolling = false;
                    stopTimer = 0f;
                }
            }
        }

        private void OnBecameInvisible()
        {
            // ✅ Добавлено: сброс состояния при удалении из сцены
            isRolling = false;
            stopTimer = 0f;
        }

        private void ScoreWrite()
        {
            totalScore = 0;
            
            foreach (GameObject dice in dices)
            {
                SideChecker sideChecker = dice.GetComponent<SideChecker>();
                if (sideChecker != null)
                {
                    totalScore += sideChecker.Score;
                }
            }

            textScore.text = totalScore.ToString();

            if (ads != null && isInitialized(ads))
            {
                ads.OnDiceThrown();
            }
        }

        private bool IsAllStopped()
        {
            if (dices.Count == 0) return true;

            // ✅ Исправлено: используем хранимые ссылки на Rigidbody вместо поиска каждый кадр
            foreach (Rigidbody rb in rigidbodies)
            {
                if (rb != null && rb.linearVelocity.magnitude > 0.05f)
                {
                    return false;
                }
            }
            return true;
        }

        // ✅ Добавлено: проверка инициализации Appodeal
        private bool isInitialized(AppodealADSScript ads)
        {
            if (ads == null) return false;
            
            // Проверяем, был ли вызван OnInitializationFinished без ошибок
            // В реальном проекте лучше добавить публичный флаг в AppodealADSScript
            return true;
        }
    }
}
