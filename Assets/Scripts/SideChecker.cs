using UnityEngine;

namespace LuckyDice
{
    public class SideChecker : MonoBehaviour
    {
        [SerializeField] private Transform[] faces;
        [SerializeField] private GameObject dice;
        [SerializeField] private float velocityThreshold = 0.05f;

        public int Score { get; private set; }

        private Rigidbody rb;
        private bool isMoving;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            isMoving = rb.linearVelocity.magnitude > velocityThreshold;

            if (isMoving)
            {
                Score = CheckFace();
            }
        }

        private int CheckFace()
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

            int total = System.Array.IndexOf(faces, upFace) + 1;

            if (dice.CompareTag("D10") && total == 10)
            {
                total = 0;
            }

            return total;
        }
    }
}
