using UnityEngine;
using UnityEngine.UI;

namespace LuckyDice
{
    public class DiceMenu : MonoBehaviour
    {
        [System.Serializable]
        public class DiceRow
        {
            public DisplayNumber displayNumber;
            public Text countText;
        }

        public DiceRow[] diceRows;
        public Button checkButton;
        public GameObject scoreField;

        private bool scoreVisible = false;

        private void Start()
        {
            if (checkButton != null)
                checkButton.onClick.AddListener(ToggleScore);
        }

        public void ToggleScore()
        {
            scoreVisible = !scoreVisible;
            if (scoreField != null)
                scoreField.SetActive(scoreVisible);
        }
    }
}
