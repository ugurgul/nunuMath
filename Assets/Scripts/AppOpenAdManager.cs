using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AppOpenAdManager : MonoBehaviour
{   
    
    private bool isShowingAd = false;
    // Start is called before the first frame update
    #if UNITY_ANDROID
    private const string AD_UNIT_ID = "ca-app-pub-7558593416027654/5025873032";
    #elif UNITY_IOS
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5662855259";
    #else
    private const string AD_UNIT_ID = "unexpected_platform";
    #endif
    private AppOpenAd ad;
    int uyelikDurumu = 0;

    private void Awake() {
            if(PlayerPrefs.HasKey("uyelikDurumu"))
        {
            uyelikDurumu = PlayerPrefs.GetInt("uyelikDurumu",0);
        }
    }

    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        //uyelikDurumu =0;
        

         if(uyelikDurumu == 0)
         {
            MobileAds.Initialize(initStatus => { });
            LoadAd();
            ShowAdIfAvailable();
         }
            
    }

private bool IsAdAvailable
    {
        get
        {
            return ad != null;
        }
    }
    public void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        AppOpenAd.Load(AD_UNIT_ID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                return;
            }

            // App open ad is loaded.
            ad = appOpenAd;
        }));
    }

     public void ShowAdIfAvailable()

    {
        if (!IsAdAvailable || isShowingAd)
        {
        return;
    }

        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();


    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        //Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        //Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        //Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)

    {
        //Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                //args.AdValue.CurrencyCode, args.AdValue.Value);
    }


    public void OnApplicationPause(bool pauseStatus) {

         if(uyelikDurumu == 0)
         {        
            if(!pauseStatus)
         {
            ShowAdIfAvailable();
         }

         }

        
    }
}
