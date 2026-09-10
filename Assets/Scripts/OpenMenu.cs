using UnityEngine;

namespace LuckyDice
{
    /// <summary>
    /// Управление открытием/закрытием меню настроек по кнопке setting_btn
    /// </summary>
    public class OpenMenu : MonoBehaviour
    {
        [Header("Ссылки на UI элементы")]
        public GameObject Setting_bg;              // Фон меню
        
        [Header("Ссылки на скрипты")]
        public DiceMenu diceMenu;                  // Скрипт управления меню

        private bool isMenuOpen = false;            // Текущее состояние меню

        private void Start()
        {
            // По умолчанию меню закрыто
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(false);
            }
            
            // Если есть ссылка на DiceMenu, устанавливаем начальное состояние
            if (diceMenu != null)
            {
                diceMenu.SetMenuEnabled(false);
            }
        }

        /// <summary>
        /// Открывает/закрывает меню настроек по кнопке setting_btn
        /// </summary>
        public void OpenM()
        {
            if (Setting_bg == null) return;
            
            // Переключаем состояние меню
            isMenuOpen = !isMenuOpen;
            
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(isMenuOpen);
            }
            
            // Если есть ссылка на DiceMenu, обновляем его состояние
            if (diceMenu != null)
            {
                diceMenu.SetMenuEnabled(isMenuOpen);
            }
        }

        /// <summary>
        /// Принудительно открывает меню
        /// </summary>
        public void ForceOpen()
        {
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(true);
            }
            
            if (diceMenu != null)
            {
                diceMenu.SetMenuEnabled(true);
            }
            
            isMenuOpen = true;
        }

        /// <summary>
        /// Принудительно закрывает меню
        /// </summary>
        public void ForceClose()
        {
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(false);
            }
            
            if (diceMenu != null)
            {
                diceMenu.SetMenuEnabled(false);
            }
            
            isMenuOpen = false;
        }

        /// <summary>
        /// Проверяет, открыто ли меню
        /// </summary>
        public bool IsMenuOpen()
        {
            return isMenuOpen && Setting_bg != null && Setting_bg.activeSelf;
        }
    }
}
