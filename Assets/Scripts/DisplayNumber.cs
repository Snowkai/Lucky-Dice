using UnityEngine;
using UnityEngine.UI;

namespace LuckyDice
{
    public class DisplayNumber : MonoBehaviour
    {
        public Button plus;
        public Button minus;
        public Text countText;
        public GameObject dice;
        public int initialDiceCount = 0;

        private int number_dices = 0;
        private GameObject[] destrobj;
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
            if (number_dices < 6)
            {
                ++number_dices;
                if (countText != null)
                    countText.text = number_dices.ToString();
                Instantiate(dice);
            }
        }

        public void btn_minus()
        {
            if (number_dices > 0)
            {
                --number_dices;
                if (countText != null)
                    countText.text = number_dices.ToString();
                destrobj = GameObject.FindGameObjectsWithTag(dice.tag);
                Destroy(destrobj[destrobj.Length - 1]);
            }
        }
    }
}
