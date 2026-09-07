using UnityEngine;

namespace LuckyDice
{
    public class OpenMenu : MonoBehaviour
    {
        public GameObject Setting_bg;

        public void OpenM()
        {
            if (Setting_bg != null)
            {
                Setting_bg.SetActive(!Setting_bg.activeSelf);
            }
        }
    }
}
