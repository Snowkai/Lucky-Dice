using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyDice
{
    public class ScoreChecker : MonoBehaviour
    {
        public AppodealADSScript ads;
        public Text textScore;

        private List<GameObject> dices = new List<GameObject>();
        private string[] tags = new string[] { "D4", "D6", "D8", "D10", "D12", "D20" };
        private int totalScore = 0;

        private bool isRolling = false;
        private float stopTimer = 0f;
        private float timeToWait = 0.8f;

        private void Update()
        {
            UpdateDiceList();
            bool allStopped = IsAllStopped();

            if (!allStopped)
            {
                isRolling = true;
                stopTimer = 0f;
            }
            else if (isRolling && allStopped)
            {
                stopTimer += Time.deltaTime;

                if (stopTimer >= timeToWait)
                {
                    ScoreWrite();
                    isRolling = false;
                    stopTimer = 0f;
                }
            }
        }

        private void ScoreWrite()
        {
            totalScore = 0;
            foreach (GameObject dice in dices)
            {
                SideChecker sideChecker = dice.GetComponent<SideChecker>();
                if (sideChecker != null)
                {
                    totalScore += sideChecker.Score;
                }
            }

            textScore.text = totalScore.ToString();

            if (ads != null)
            {
                ads.OnDiceThrown();
            }
        }

        private void UpdateDiceList()
        {
            dices.Clear();
            foreach (string tag in tags)
            {
                GameObject[] found = GameObject.FindGameObjectsWithTag(tag);
                dices.AddRange(found);
            }
        }

        private bool IsAllStopped()
        {
            if (dices.Count == 0) return true;

            foreach (GameObject dice in dices)
            {
                Rigidbody rb = dice.GetComponent<Rigidbody>();
                if (rb != null && rb.linearVelocity.magnitude > 0.05f)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
