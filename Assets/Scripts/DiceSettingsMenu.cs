using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace LuckyDice
{
    /// <summary>
    /// Меню настроек количества кубиков для всех типов (D4, D6, D8, D10, D12, D20)
    /// </summary>
    public class DiceSettingsMenu : MonoBehaviour
    {
        [System.Serializable]
        public class DiceTypeRow
        {
            public Text typeLabel;           // D4, D6, D8, D10, D12, D20
            public Button minusButton;       // Кнопка уменьшения
            public Image minusButtonImage;   // Изображение кнопки минус
            public Sprite minusSprite;       // Спрайт для кнопки минус
            public Text countText;           // Поле отображения количества
            public Button plusButton;        // Кнопка увеличения
            public Image plusButtonImage;    // Изображение кнопки плюс
            public Sprite plusSprite;        // Спрайт для кнопки плюс
            public GameObject dicePrefab;    // Префаб кубика этого типа
            public int maxDiceCount = 6;     // Максимальное количество кубиков
        }

        [Header("Настройки меню")]
        public DiceTypeRow[] diceRows;       // Массив строк с типами кубиков
        public GameObject Setting_bg;        // Фон меню настроек
        
        [Header("Спрайты кнопок")]
        public Sprite minusSprite;           // Спрайт кнопки минус
        public Sprite plusSprite;            // Спрайт кнопки плюс
        public Sprite buttonHoverSprite;     // Спрайт кнопки при наведении
        public Sprite buttonPressedSprite;   // Спрайт кнопки при нажатии
        
        [Header("Спрайты состояния")]
        public Sprite toggleOnSprite;        // Спрайт включено (галочка)
        public Sprite toggleOffSprite;       // Спрайт выключено
        
        [Header("Отображение счета")]
        public GameObject scoreField;        // Поле для отображения счета
        private bool scoreVisible = false;

        private Dictionary<GameObject, int> diceCounts = new Dictionary<GameObject, int>();
        private bool hasSpawned = false;

        private void Start()
        {
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(!Setting_bg.activeSelf);
            }

            // Инициализация количества кубиков для каждого типа
            InitializeDiceCounts();

            // Подключение кнопок переключения счета
            if (scoreField != null)
            {
                scoreVisible = scoreField.activeSelf;
            }
        }

        /// <summary>
        /// Инициализирует начальное количество кубиков для всех типов
        /// </summary>
        private void InitializeDiceCounts()
        {
            if (diceRows == null || diceRows.Length == 0) return;

            foreach (var row in diceRows)
            {
                if (row.countText != null && row.dicePrefab != null)
                {
                    row.countText.text = "0";
                    diceCounts[row.dicePrefab] = 0;
                }
            }
        }

        /// <summary>
        /// Обновляет количество кубиков для конкретного типа
        /// </summary>
        public void UpdateDiceCount(GameObject dicePrefab, int count)
        {
            if (dicePrefab == null || countTextForPrefab(dicePrefab) == null) return;

            diceCounts[dicePrefab] = Mathf.Max(0, Mathf.Min(count, diceCounts[dicePrefab]));
            
            // Обновляем текст в поле
            Text countText = countTextForPrefab(dicePrefab);
            if (countText != null)
            {
                countText.text = diceCounts[dicePrefab].ToString();
            }
        }

        /// <summary>
        /// Получает текстовое поле для конкретного типа кубика
        /// </summary>
        private Text countTextForPrefab(GameObject dicePrefab)
        {
            if (diceRows != null)
            {
                foreach (var row in diceRows)
                {
                    if (row.dicePrefab == dicePrefab && row.countText != null)
                    {
                        return row.countText;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Добавляет кубик указанного типа на сцену
        /// </summary>
        public void AddDice(GameObject dicePrefab)
        {
            if (dicePrefab == null || hasSpawned) return;
            
            // Проверяем максимальное количество для этого типа
            int currentCount = 0;
            foreach (var row in diceRows)
            {
                if (row.dicePrefab == dicePrefab)
                {
                    currentCount = diceCounts[dicePrefab];
                    break;
                }
            }

            if (currentCount >= 6)
            {
                return; // Достигнуто максимальное количество
            }

            ++currentCount;
            diceCounts[dicePrefab] = currentCount;

            // Спавним кубик
            GameObject newDice = Instantiate(dicePrefab);
            
            // Обновляем текст в поле
            Text countText = countTextForPrefab(dicePrefab);
            if (countText != null)
            {
                countText.text = currentCount.ToString();
            }

            hasSpawned = true;
        }

        /// <summary>
        /// Удаляет кубик указанного типа со сцены
        /// </summary>
        public void RemoveDice(GameObject dicePrefab)
        {
            if (dicePrefab == null || !hasSpawned) return;

            int currentCount = 0;
            foreach (var row in diceRows)
            {
                if (row.dicePrefab == dicePrefab)
                {
                    currentCount = diceCounts[dicePrefab];
                    break;
                }
            }

            if (currentCount <= 0) return; // Нельзя удалить, если кубиков нет

            --currentCount;
            diceCounts[dicePrefab] = currentCount;

            // Удаляем последний спавненный кубик этого типа
            GameObject[] allDices = FindObjectsOfType<GameObject>();
            foreach (GameObject dice in allDices)
            {
                if (dicePrefab != null && dicePrefab.name == dice.name)
                {
                    Destroy(dice);
                    break;
                }
            }

            // Обновляем текст в поле
            Text countText = countTextForPrefab(dicePrefab);
            if (countText != null)
            {
                countText.text = currentCount.ToString();
            }
        }

        /// <summary>
        /// Переключает отображение счета
        /// </summary>
        public void ToggleScore()
        {
            scoreVisible = !scoreVisible;
            if (scoreField != null)
            {
                scoreField.SetActive(scoreVisible);
            }
            UpdateToggleSprite();
        }

        private void UpdateToggleSprite()
        {
            // Можно добавить логику обновления спрайта кнопки переключения
        }

        /// <summary>
        /// Очищает все кубики со сцены
        /// </summary>
        public void ClearAllDices()
        {
            if (!hasSpawned) return;

            GameObject[] allDices = FindObjectsOfType<GameObject>();
            foreach (GameObject dice in allDices)
            {
                Destroy(dice);
            }

            // Сбрасываем все количества в 0
            InitializeDiceCounts();
            hasSpawned = false;
        }

        /// <summary>
        /// Получает текущее количество кубиков для конкретного типа
        /// </summary>
        public int GetDiceCount(GameObject dicePrefab)
        {
            if (dicePrefab == null) return 0;
            
            foreach (var row in diceRows)
            {
                if (row.dicePrefab == dicePrefab)
                {
                    return diceCounts[dicePrefab];
                }
            }
            return 0;
        }

        /// <summary>
        /// Получает все активные кубики по типу
        /// </summary>
        public GameObject[] GetActiveDices(GameObject dicePrefab)
        {
            if (dicePrefab == null || !hasSpawned) return new GameObject[0];

            int count = 0;
            foreach (var row in diceRows)
            {
                if (row.dicePrefab == dicePrefab)
                {
                    count = diceCounts[dicePrefab];
                    break;
                }
            }

            GameObject[] result = new GameObject[count];
            GameObject[] allDices = FindObjectsOfType<GameObject>();
            
            int index = 0;
            foreach (GameObject dice in allDices)
            {
                if (dicePrefab != null && dicePrefab.name == dice.name && index < count)
                {
                    result[index++] = dice;
                }
            }

            return result;
        }

        private void OnDestroy()
        {
            // Очистка всех кубиков при уничтожении скрипта
            ClearAllDices();
        }
    }
}
