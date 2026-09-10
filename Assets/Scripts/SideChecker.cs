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
        private Quaternion lastRotation;  // ✅ Добавлено: храним последнюю ориентацию для сравнения

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                lastRotation = rb.rotation;  // ✅ Инициализация последней ориентации
            }
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
            // ✅ Исправлено: определение грани по Euler angles вместо Y-координаты
            float eulerY = Mathf.Round(rb.rotation.eulerAngles.y / 90f) * 90f;
            
            // D20: 0° = грань 1, 90° = грань 2, ...
            int faceValue = (int)(eulerY / 90f) + 1;

            // D10: 0° = грань 1, 180° = грань 2 (или 0)
            if (dice.CompareTag("D10"))
            {
                faceValue = (faceValue % 2 == 0) ? 0 : faceValue;
            }

            // D4: 0° = грань 1, 90° = грань 2
            if (dice.CompareTag("D4"))
            {
                faceValue = (faceValue % 2 == 0) ? faceValue / 2 : faceValue;
            }

            // D6: 0° = грань 1, 90° = грань 2, ...
            if (dice.CompareTag("D6"))
            {
                faceValue = (faceValue % 4 == 0) ? faceValue / 4 : faceValue;
            }

            // D8: 0° = грань 1, 90° = грань 2, ...
            if (dice.CompareTag("D8"))
            {
                faceValue = (faceValue % 4 == 0) ? faceValue / 4 : faceValue;
            }

            // D12: 0° = грань 1, 90° = грань 2, ...
            if (dice.CompareTag("D12"))
            {
                faceValue = (faceValue % 6 == 0) ? faceValue / 6 : faceValue;
            }

            return faceValue;
        }

        private void OnBecameInvisible()
        {
            // Сброс Score при удалении из сцены
            Score = 0;
        }
    }
}
