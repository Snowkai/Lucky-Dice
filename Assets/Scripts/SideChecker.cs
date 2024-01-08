using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SideChecker : MonoBehaviour
{
    public Transform[] faces;
    public int score=0;
    public GameObject dice;

    // проверка верхней крани кости.
    private int checkFace()
    {
        float maxY = 0f;
        Transform upFace = null;

        foreach (Transform face in faces)
        {
            if (face.position.y > maxY)
            {
                maxY = face.position.y;
                upFace = face;
            }
        }

        int total = Array.IndexOf(faces, upFace) + 1;

        if (dice.tag == "D10")
        {
            if(total == 10)
            {
                total = 0;
            }
        }
        
        return total;
    }

    private void Update()
    {
        score = checkFace();
    }
}
