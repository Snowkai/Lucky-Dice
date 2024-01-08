using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHelper : MonoBehaviour
{
    private Vector3 DicePosition;
    private Vector3 MousePos;
    private Vector3 CurrentPos;
    private Vector3 direction;
    public float speed;
    public float torq;

    // Start is called before the first frame update
    void Start()
    {
        DicePosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.z, Input.mousePosition.y);
            DicePosition = new Vector3(transform.position.x, 4, transform.position.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CurrentPos = new Vector3(Input.mousePosition.x, Input.mousePosition.z, Input.mousePosition.y);
            direction = CurrentPos - MousePos;
            transform.position = DicePosition;
            GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddTorque(direction, ForceMode.Impulse);
        }

        
    }
}
