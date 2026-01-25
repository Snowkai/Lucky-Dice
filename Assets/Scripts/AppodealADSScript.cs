using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;

public class AppodealADSScript : MonoBehaviour
{
    private int throwCounter = 0; // Счетчик бросков
    private const int ThrowsBeforeAd = 7; // Через сколько бросков показывать

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Тестовый ркжим
        //Appodeal.SetTesting(true);
        
        int adTypes = AppodealAdType.Interstitial;
        string appKey = "da749c03d21bba586171f1273db7e71922ab176c67c7710a";
        AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
        Appodeal.Initialize(appKey, adTypes);
    }

    public void OnInitializationFinished(object sender, SdkInitializedEventArgs e) { }

    // Этот метод вызывай каждый раз, когда игрок бросает кубик
    public void OnDiceThrown()
    {
        throwCounter++;
        Debug.Log($"Бросок №{throwCounter}"); // Для отладки в консоли

        if (throwCounter >= ThrowsBeforeAd)
        {
            ShowInterstitial();
        }
    }

    private void ShowInterstitial()
    {
        if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
        {
            Appodeal.Show(AppodealShowStyle.Interstitial);
            //Debug.Log("ADS SHOW");
            throwCounter = 0; // Сброс, так как реклама показана
        }
        else
        {
            //Debug.Log("Реклама еще не прогрузилась. Попробуем на следующем броске.");
            // Если хотите сбросить и ждать еще 3 броска, 
            // добавьте throwCounter = 0; и здесь тоже.
            throwCounter = 0;
        }
    }

}
