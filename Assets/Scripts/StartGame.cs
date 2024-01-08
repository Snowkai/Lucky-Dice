using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Button>().onClick.Invoke();
        button.GetComponent<Button>().onClick.Invoke();
    }

 
}
