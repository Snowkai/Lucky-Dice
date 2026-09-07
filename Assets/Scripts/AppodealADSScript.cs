using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;

namespace LuckyDice
{
    public class AppodealADSScript : MonoBehaviour
    {
        [SerializeField] private int throwsBeforeAd = 7;

        private int throwCounter;

        private void Start()
        {
            int adTypes = AppodealAdType.Interstitial;
            string appKey = "da749c03d21bba586171f1273db7e71922ab176c67c7710a";

            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            Appodeal.Initialize(appKey, adTypes);
        }

        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e) { }

        public void OnDiceThrown()
        {
            throwCounter++;
            Debug.Log($"Бросок №{throwCounter}");

            if (throwCounter >= throwsBeforeAd)
            {
                ShowInterstitial();
            }
        }

        private void ShowInterstitial()
        {
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
            {
                Appodeal.Show(AppodealShowStyle.Interstitial);
            }

            throwCounter = 0;
        }
    }
}
