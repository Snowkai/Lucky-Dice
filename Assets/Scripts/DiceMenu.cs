using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace LuckyDice
{
    /// <summary>
    /// Управление меню настроек кубиков (2/3 экрана, запускается по setting_btn)
    /// </summary>
    public class DiceMenu : MonoBehaviour
    {
        [Header("Ссылки на UI элементы")]
        public GameObject Setting_bg;              // Фон меню
        public Image backgroundImage;              // Изображение фона
        
        [Header("Кнопки управления")]
        public Button minusButton;                 // Кнопка уменьшения
        public Image minusButtonImage;             // Изображение кнопки минус
        public Sprite minusSprite;                 // Спрайт для кнопки минус
        public Sprite minusHoverSprite;            // Спрайт при наведении
        public Sprite minusPressedSprite;          // Спрайт при нажатии
        
        public Button plusButton;                  // Кнопка увеличения
        public Image plusButtonImage;              // Изображение кнопки плюс
        public Sprite plusSprite;                  // Спрайт для кнопки плюс
        public Sprite plusHoverSprite;             // Спрайт при наведении
        public Sprite plusPressedSprite;           // Спрайт при нажатии
        
        [Header("Поля отображения")]
        public Text countTextLegacy;               // Поле количества кубиков (legacy)
        public TextMeshProUGUI countText;          // Поле количества кубиков (TMP)
        public GameObject scoreField;              // Поле для отображения счета
        
        [Header("Спрайты состояния")]
        public Sprite toggleOnSprite;              // Спрайт включено (галочка)
        public Sprite toggleOffSprite;             // Спрайт выключено
        public Sprite dividerLineLong;             // Длинная разделительная линия
        public Sprite dividerLineShort;            // Короткая разделительная линия
        
        [Header("Оформление")]
        public GameObject parchmentPanel;          // Панель пергамента
        public Image panelShadow;                  // Тень панели
        public GameObject titleOrnament;           // Орнамент заголовка
        
        [Header("Настройки")]
        public int defaultDiceCount = 2;           // Начальное количество кубиков по умолчанию
        public bool menuEnabled = false;            // Состояние меню (включено/выключено)
        
        [Header("Dice Prefab")]
        public GameObject dicePrefab;               // Префаб кубика для спавна
        
        private Dictionary<GameObject, int> diceCounts = new Dictionary<GameObject, int>();
        private bool hasSpawned = false;
        private bool scoreVisible = false;

        private void SetCountText(string value)
        {
            if (countText != null) countText.text = value;
            if (countTextLegacy != null) countTextLegacy.text = value;
        }

        private void Start()
        {
            // Инициализация количества кубиков для каждого типа
            InitializeDiceCounts();
            
            // Подключение кнопок
            if (minusButton != null)
                minusButton.onClick.AddListener(OnMinusClicked);
                
            if (plusButton != null)
                plusButton.onClick.AddListener(OnPlusClicked);
                
            if (scoreField != null)
            {
                scoreVisible = scoreField.activeSelf;
            }
            
            // По умолчанию меню отключено, 2 кубика D6 на сцене
            menuEnabled = false;
            SpawnDefaultDices();
        }

        /// <summary>
        /// Инициализирует начальное количество кубиков для всех типов
        /// </summary>
        private void InitializeDiceCounts()
        {
            if (diceCounts.Count > 0) return; // Уже инициализировано
            
            // Создаем словарь для хранения количества кубиков каждого типа
            // В реальном проекте здесь можно найти все типы кубиков на сцене
        }

        /// <summary>
        /// Спавнит по умолчанию 2 кубика D6 при старте
        /// </summary>
        public void SpawnDefaultDices()
        {
            if (hasSpawned) return;
            if (dicePrefab == null)
            {
                Debug.LogWarning("[DiceMenu] dicePrefab is null, cannot spawn default dices");
                return;
            }
            
            hasSpawned = true;
            diceCounts[dicePrefab] = defaultDiceCount;
            
            for (int i = 0; i < defaultDiceCount; i++)
            {
                GameObject newDice = Instantiate(dicePrefab);
                newDice.name = dicePrefab.name;
                newDice.transform.position = new Vector3(
                    Random.Range(-2f, 2f),
                    Random.Range(1f, 3f),
                    Random.Range(-2f, 2f));
            }
            
            SetCountText(defaultDiceCount.ToString());
            
            Debug.Log("[DiceMenu] Spawned " + defaultDiceCount + " default dices");
        }

        /// <summary>
        /// Добавляет кубик указанного типа на сцену
        /// </summary>
        public void AddDice(GameObject dicePrefab)
        {
            if (dicePrefab == null || !menuEnabled) return;
            
            // Проверяем максимальное количество для этого типа
            int currentCount = 0;
            foreach (var row in diceCounts)
            {
                if (row.Key != null && row.Key.name == dicePrefab.name)
                {
                    currentCount = row.Value;
                    break;
                }
            }

            // Максимальное количество кубиков - 6
            int maxDiceCount = 6;
            
            if (currentCount >= maxDiceCount)
            {
                return; // Достигнуто максимальное количество
            }

            ++currentCount;
            
            // Обновляем словарь
            if (!diceCounts.ContainsKey(dicePrefab))
            {
                diceCounts[dicePrefab] = 0;
            }
            diceCounts[dicePrefab] = currentCount;

            // Спавним кубик
            GameObject newDice = Instantiate(dicePrefab);
            
            // Обновляем текст в поле
            SetCountText(currentCount.ToString());
        }

        /// <summary>
        /// Удаляет кубик указанного типа со сцены
        /// </summary>
        public void RemoveDice(GameObject dicePrefab)
        {
            if (dicePrefab == null || !menuEnabled) return;

            int currentCount = 0;
            foreach (var row in diceCounts)
            {
                if (row.Key != null && row.Key.name == dicePrefab.name)
                {
                    currentCount = row.Value;
                    break;
                }
            }

            if (currentCount <= 0) return; // Нельзя удалить, если кубиков нет

            --currentCount;
            
            // Обновляем словарь
            if (diceCounts.ContainsKey(dicePrefab))
            {
                diceCounts[dicePrefab] = currentCount;
            }

            // Удаляем последний спавненный кубик этого типа
            GameObject[] allDices = FindObjectsOfType<GameObject>();
            foreach (GameObject dice in allDices)
            {
                if (dicePrefab != null && dice.name == dicePrefab.name)
                {
                    Destroy(dice);
                    break;
                }
            }

            // Обновляем текст в поле
            SetCountText(currentCount.ToString());
        }

        private void OnMinusClicked()
        {
            foreach (var kvp in diceCounts)
            {
                if (kvp.Value > 0)
                {
                    RemoveDice(kvp.Key);
                    return;
                }
            }
        }

        private void OnPlusClicked()
        {
            foreach (var kvp in diceCounts)
            {
                AddDice(kvp.Key);
                return;
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
            if (toggleOnSprite != null && toggleOffSprite != null)
            {
                // Логика обновления спрайта
            }
        }

        /// <summary>
        /// Очищает все кубики со сцены
        /// </summary>
        public void ClearAllDices()
        {
            if (!menuEnabled) return;

            GameObject[] allDices = FindObjectsOfType<GameObject>();
            foreach (GameObject dice in allDices)
            {
                Destroy(dice);
            }

            // Сбрасываем все количества в 0
            diceCounts.Clear();
            hasSpawned = false;
            
            SetCountText("0");
        }

        /// <summary>
        /// Получает текущее количество кубиков для конкретного типа
        /// </summary>
        public int GetDiceCount(GameObject dicePrefab)
        {
            if (dicePrefab == null || !menuEnabled) return 0;
            
            foreach (var row in diceCounts)
            {
                if (row.Key != null && row.Key.name == dicePrefab.name)
                {
                    return row.Value;
                }
            }
            return 0;
        }

        /// <summary>
        /// Получает все активные кубики по типу
        /// </summary>
        public GameObject[] GetActiveDices(GameObject dicePrefab)
        {
            if (dicePrefab == null || !menuEnabled) return new GameObject[0];

            int count = 0;
            foreach (var row in diceCounts)
            {
                if (row.Key != null && row.Key.name == dicePrefab.name)
                {
                    count = row.Value;
                    break;
                }
            }

            GameObject[] result = new GameObject[count];
            GameObject[] allDices = FindObjectsOfType<GameObject>();
            
            int index = 0;
            foreach (GameObject dice in allDices)
            {
                if (dicePrefab != null && dice.name == dicePrefab.name && index < count)
                {
                    result[index++] = dice;
                }
            }

            return result;
        }

        /// <summary>
        /// Обновляет состояние меню (включить/выключить)
        /// </summary>
        public void SetMenuEnabled(bool enabled)
        {
            menuEnabled = enabled;
            
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(enabled);
            }
            
            // Обновляем спрайты кнопок в зависимости от состояния меню
            UpdateButtonSprites();
        }

        /// <summary>
        /// Обновляет спрайты кнопок в зависимости от состояния меню
        /// </summary>
        private void UpdateButtonSprites()
        {
            if (minusButtonImage != null)
            {
                minusButtonImage.sprite = menuEnabled ? minusSprite : toggleOffSprite;
            }
            
            if (plusButtonImage != null)
            {
                plusButtonImage.sprite = menuEnabled ? plusSprite : toggleOffSprite;
            }
        }

        private void OnDestroy()
        {
            // Очистка всех кубиков при уничтожении скрипта
            ClearAllDices();
        }
    }
}
