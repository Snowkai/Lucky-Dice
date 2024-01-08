using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject Setting_bg;

    public void OpenM()
    {
        if(Setting_bg != null)
        {
            bool isActive = Setting_bg.activeSelf;

            Setting_bg.SetActive(!isActive);
        }

    }
}
