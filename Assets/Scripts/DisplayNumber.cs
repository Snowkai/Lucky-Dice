using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;  // ✅ Добавлено: требуется для List<T>

namespace LuckyDice
{
    public class DisplayNumber : MonoBehaviour
    {
        public Button plus;
        public Button minus;
        public Text countText;
        public GameObject dicePrefab;  // ✅ Исправлено: теперь используем Prefab
        public int initialDiceCount = 0;

        private int number_dices = 0;
        private List<GameObject> activeDices = new List<GameObject>();  // ✅ Исправлено: храним все активные кости
        private bool hasSpawned = false;

        private void OnEnable()
        {
            if (hasSpawned) return;
            hasSpawned = true;

            for (int i = 0; i < initialDiceCount; i++)
            {
                btn_plus();
            }
        }

        public void btn_plus()
        {
            if (number_dices < 6 && dicePrefab != null)
            {
                ++number_dices;
                if (countText != null)
                    countText.text = number_dices.ToString();
                
                GameObject newDice = Instantiate(dicePrefab);
                activeDices.Add(newDice);  // ✅ Исправлено: добавляем в список активных костей
            }
        }

        public void btn_minus()
        {
            if (number_dices > 0)
            {
                --number_dices;
                if (countText != null)
                    countText.text = number_dices.ToString();
                
                if (activeDices.Count > 0)
                {
                    GameObject diceToRemove = activeDices[activeDices.Count - 1];  // ✅ Исправлено: берём последний из списка
                    Destroy(diceToRemove);
                    activeDices.RemoveAt(activeDices.Count - 1);  // ✅ Исправлено: удаляем из списка
                }
            }
        }

        private void OnDestroy()
        {
            // Очистка всех активных костей при уничтожении скрипта
            foreach (GameObject dice in activeDices)
            {
                Destroy(dice);
            }
            activeDices.Clear();
        }
    }
}
