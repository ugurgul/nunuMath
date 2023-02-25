using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;


public class AdController : MonoBehaviour
{
    public BannerView bannerView;
    public InterstitialAd interstitial;

    public void Start()
    {
        //Current = this;
        MobileAds.Initialize(initStatus => { });
        //this.RequestBanner();
        this.RequestInterstitial();


    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-7558593416027654/6338954700";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

         // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void ShowBanner(){
        this.RequestBanner();
        bannerView.Show();
    }

    private void RequestInterstitial()
{
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7558593416027654/4839553665";
    #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
    #else
        string adUnitId = "unexpected_platform";
    #endif

    // Initialize an InterstitialAd.
    this.interstitial = new InterstitialAd(adUnitId);

    // Called when an ad request failed to load.
    this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    // Called when an ad is shown.
    this.interstitial.OnAdOpening += HandleOnAdOpening;
    // Called when the ad is closed.
    this.interstitial.OnAdClosed += HandleOnAdClosed;
    
    AdRequest request = new AdRequest.Builder().Build();

    

    // Load the interstitial with the request.
    this.interstitial.LoadAd(request);
}


public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
{
    //Time.timeScale = 1;
    RequestInterstitial();
}

public void HandleOnAdOpening(object sender, EventArgs args)
{
    //Time.timeScale = 0;
}

public void HandleOnAdClosed(object sender, EventArgs args)
{
   RequestInterstitial();
}

public void ShowInterstitial()
{

    if (interstitial.CanShowAd()) {
    
        interstitial.Show();
    }

}

}
