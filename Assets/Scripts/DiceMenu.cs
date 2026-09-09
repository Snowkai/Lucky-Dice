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
        public Image checkButtonImage;
        public Sprite toggleOnSprite;
        public Sprite toggleOffSprite;
        public GameObject scoreField;

        private bool scoreVisible = false;

        private void Start()
        {
            if (checkButton != null)
                checkButton.onClick.AddListener(ToggleScore);

            scoreVisible = scoreField != null && scoreField.activeSelf;
            UpdateToggleSprite();
        }

        public void ToggleScore()
        {
            scoreVisible = !scoreVisible;
            if (scoreField != null)
                scoreField.SetActive(scoreVisible);
            UpdateToggleSprite();
        }

        private void UpdateToggleSprite()
        {
            if (checkButtonImage != null)
            {
                checkButtonImage.sprite = scoreVisible ? toggleOnSprite : toggleOffSprite;
            }
        }
    }
}