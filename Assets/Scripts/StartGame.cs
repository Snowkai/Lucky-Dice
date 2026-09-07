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
            if (button != null)
            {
                Button btn = button.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke();
                }
            }

            if (Setting_bg != null)
            {
                Setting_bg.SetActive(false);
            }
        }
    }
}
