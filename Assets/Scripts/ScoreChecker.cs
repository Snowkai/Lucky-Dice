using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ScoreChecker : MonoBehaviour
{
    public Text textScore;
    private List<GameObject> dices = new List<GameObject>();
    private string[] tags = new string[] { "D4", "D6", "D8", "D10", "D12", "D20" };
    private int totalScore = 0;
    void Update()
    {
        CreateListOfDices();
        
        if (cheker(dices))
        {
            scoreWrite();
            dices.Clear();
            
        }
        else
        {
            dices.Clear();     
        }  
    }

    private void scoreWrite()
    {
        foreach (GameObject dice in dices)
        {
            totalScore += dice.GetComponent<SideChecker>().score;
        }

        textScore.text = totalScore.ToString();
        totalScore = 0;       
    }

    private void CreateListOfDices()
    {
        foreach (string tag in tags)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                dices.Add(obj);
            }
        }
    }

    bool cheker(List<GameObject> dices)
    {
        bool check = false;
        foreach (GameObject dice in dices)
        {
            if (dice.GetComponent<Rigidbody>().velocity.magnitude == 0)
            {
                check = true;
            }
            else
            {
                check = false;
            }
        }
        return check;
    }
}
