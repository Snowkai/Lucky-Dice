using UnityEngine;
using UnityEngine.UI;

namespace LuckyDice
{
    public class StartGame : MonoBehaviour
    {
        public GameObject button;
        public GameObject Setting_bg;

        private void Start()
        {
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(true);
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
    }
}