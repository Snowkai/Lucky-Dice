using UnityEngine;

namespace LuckyDice
{
    public class DiceHelper : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float torque = 5f;
        [SerializeField] private float spawnHeight = 4f;

        private Rigidbody rb;
        private Vector3 spawnPosition;
        private Vector3 mouseDownPos;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            spawnPosition = transform.position;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPos = new Vector3(Input.mousePosition.x, Input.mousePosition.z, Input.mousePosition.y);
                spawnPosition = new Vector3(transform.position.x, spawnHeight, transform.position.z);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mouseUpPos = new Vector3(Input.mousePosition.x, Input.mousePosition.z, Input.mousePosition.y);
                Vector3 direction = mouseUpPos - mouseDownPos;

                transform.position = spawnPosition;
                rb.AddForce(direction * speed, ForceMode.Impulse);
                rb.AddTorque(direction * torque, ForceMode.Impulse);
            }
        }
    }
}
