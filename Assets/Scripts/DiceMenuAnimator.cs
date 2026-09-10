using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LuckyDice
{
    public class DiceMenuAnimator : MonoBehaviour
    {
        [Header("References")]
        public GameObject Setting_bg;
        public Image parchmentImage;
        public GameObject woodBackground;

        [Header("Animation Settings")]
        public float openDuration = 0.5f;
        public float closeDuration = 0.3f;
        public float scaleWhenOpen = 1f;
        public float scaleWhenClosed = 0.95f;

        [Header("Background")]
        public bool useWoodBackground = true;
        public Sprite woodBackgroundSprite;

        private bool isOpen = false;
        private bool isAnimating = false;
        private Coroutine activeCoroutine;

        private void Start()
        {
            if (Setting_bg != null)
                Setting_bg.SetActive(false);
        }

        public void OpenMenu()
        {
            if (isAnimating || isOpen) return;
            isAnimating = true;
            isOpen = true;

            if (Setting_bg != null)
                Setting_bg.SetActive(true);

            if (useWoodBackground && woodBackground != null)
                woodBackground.SetActive(true);

            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(AnimateOpen());
        }

        public void CloseMenu()
        {
            if (isAnimating || !isOpen) return;
            isAnimating = true;

            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(AnimateClose());
        }

        private IEnumerator AnimateOpen()
        {
            if (parchmentImage == null) { isAnimating = false; yield break; }

            parchmentImage.transform.localScale = Vector3.one * scaleWhenClosed;
            float elapsed = 0f;

            while (elapsed < openDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / openDuration);
                // OutBack ease approximation
                float overshoot = 1.70158f;
                t = t - 1f;
                float scale = t * t * ((overshoot + 1f) * t + overshoot) + 1f;
                scale = Mathf.Lerp(scaleWhenClosed, scaleWhenOpen, scale);
                parchmentImage.transform.localScale = Vector3.one * scale;
                yield return null;
            }

            parchmentImage.transform.localScale = Vector3.one * scaleWhenOpen;
            isAnimating = false;
            OnMenuOpened();
        }

        private IEnumerator AnimateClose()
        {
            if (parchmentImage == null)
            {
                FinishClose();
                yield break;
            }

            parchmentImage.transform.localScale = Vector3.one * scaleWhenOpen;
            float elapsed = 0f;

            while (elapsed < closeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / closeDuration);
                // InBack ease approximation
                float overshoot = 1.70158f;
                float scale = t * t * ((overshoot + 1f) * t - overshoot);
                scale = Mathf.Lerp(scaleWhenOpen, scaleWhenClosed, scale);
                parchmentImage.transform.localScale = Vector3.one * scale;
                yield return null;
            }

            parchmentImage.transform.localScale = Vector3.one * scaleWhenClosed;
            FinishClose();
        }

        private void FinishClose()
        {
            if (Setting_bg != null)
                Setting_bg.SetActive(false);

            if (woodBackground != null)
                woodBackground.SetActive(false);

            isAnimating = false;
            isOpen = false;
            OnMenuClosed();
        }

        public void ToggleMenu()
        {
            if (isOpen) CloseMenu();
            else OpenMenu();
        }

        protected virtual void OnMenuOpened() { }
        protected virtual void OnMenuClosed() { }

        public void ForceOpen()
        {
            if (Setting_bg != null)
                Setting_bg.SetActive(true);

            if (parchmentImage != null)
                parchmentImage.transform.localScale = Vector3.one * scaleWhenOpen;

            if (useWoodBackground && woodBackground != null)
                woodBackground.SetActive(true);

            isOpen = true;
            isAnimating = false;
        }

        public void ForceClose()
        {
            if (Setting_bg != null)
                Setting_bg.SetActive(false);

            if (woodBackground != null)
                woodBackground.SetActive(false);

            isOpen = false;
            isAnimating = false;
        }

        private void OnDestroy()
        {
            if (activeCoroutine != null)
                StopCoroutine(activeCoroutine);
        }
    }
}
