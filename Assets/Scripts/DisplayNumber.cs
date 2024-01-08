using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayNumber : MonoBehaviour
{
   

    public Button plus;
    public Button minus;
    public InputField display;
    private int number_dices = 0;
    public GameObject dice;
    private GameObject[] destrobj;
    
    
    

    public void btn_plus()
    {
        if (number_dices < 6)
        {
            ++number_dices;
            display.text = number_dices.ToString();
            GameObject.Instantiate<GameObject>(dice);
        } 
    }

    public void btn_minus()
    {
        if (number_dices > 0)
        {
            --number_dices;
            display.text = number_dices.ToString();
            destrobj = GameObject.FindGameObjectsWithTag(dice.tag);
            Destroy(destrobj[destrobj.Length - 1]);
        }
    }

}
