using UnityEngine;
using UnityEngine.UI;

namespace LuckyDice
{
    public class StartGame : MonoBehaviour
    {
        public GameObject button;
        public GameObject Setting_bg;
        // ✅ Добавлено: Prefab кости для использования в DisplayNumber
        public GameObject dicePrefab;

        private void Start()
        {
            // Сначала спавним кубики, пока Setting_bg ещё активен
            if (Setting_bg != null)
            {
                var diceMenu = Setting_bg.GetComponent<DiceMenu>();
                if (diceMenu != null)
                {
                    diceMenu.SpawnDefaultDices();
                }

                Setting_bg.SetActive(false);
            }

            if (button != null)
            {
                Button btn = button.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.RemoveAllListeners();
                    var openMenu = button.GetComponent<OpenMenu>();
                    if (openMenu != null)
                    {
                        btn.onClick.AddListener(() => openMenu.OpenM());
                    }
                }
            }
        }

        // ✅ Добавлено: метод для установки Prefab кости (вызывается из Editor или Inspector)
        public void SetDicePrefab(GameObject prefab)
        {
            dicePrefab = prefab;
        }
    }
}
