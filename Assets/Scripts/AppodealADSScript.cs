using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;

public class AppodealADSScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int adTypes = AppodealAdType.Interstitial;
        string appKey = "YOUR_APPODEAL_APP_KEY";
        AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
        Appodeal.Initialize(appKey, adTypes);
    }

    public void OnInitializationFinished(object sender, SdkInitializedEventArgs e) { }
}
