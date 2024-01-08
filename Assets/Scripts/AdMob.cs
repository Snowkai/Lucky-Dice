using System;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections.Generic;

public class AdMob : MonoBehaviour
{
    private BannerView bannerView;
    //real banner ID
    string adUnitId = "ca-app-pub-7811966426368723/1321637912";

    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        //MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        
        AdRequest request = new AdRequest.Builder().Build();
        
        this.bannerView.LoadAd(request);
    }
}
