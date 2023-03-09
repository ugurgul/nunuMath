#if ADS_ADMOB
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;

public class AdsAdmob : IAdLibrary
{

    private string bannerID = "";
    private string interstatialID = "";
    private string rewardedID = "";
    private string appID = "";

    private BannerView _bannerView;
    private InterstitialAd _interstatialAd;
    private RewardedAd _rewardedAd;



    public bool autoCache { get; set; }

    List<string> deviceIds = new List<string>(){"Device_Id"};

    public void Initialize(bool isTestMode)
    {
        autoCache = false;
        SetIDs();
        if(isTestMode)
        {
            //Test modu ayarları
            interstatialID = "ca-app-pub-3940256099942544/1033173712";
            rewardedID = "ca-app-pub-3940256099942544/5224354917";
            bannerID = "ca-app-pub-3940256099942544/6300978111";
        }

        deviceIds.Add(AdRequest.TestDeviceSimulator); // Emülatörde test reklamı göstermek için
        MobileAds.SetiOSAppPauseOnBackground(true); // IOS için reklam gösterildiğinde arka planda uygulamayı durdur

        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True).SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        //Initialize
        MobileAds.Initialize(OnInitializeComplete);



    }

    private void SetIDs()
    {
    #if UNITY_ANDROID
           appID = "ca-app-pub-7558593416027654~7745102385";
           bannerID = "ca-app-pub-7558593416027654/6338954700";
           interstatialID = "ca-app-pub-7558593416027654/4839553665";
           rewardedID = "rewardedID";
           
    #elif UNITY_IPHONE
           appID = "";
           bannerID = "";
           interstatialID = "";
           rewardedID = "";
           
    #endif
    }

    private void OnInitializeComplete(InitializationStatus obj)
    {
        Debug.Log("Admob Kütüphanesi yüklendi");
        MobileAdsEventExecutor.ExecuteInUpdate(()=>{AdManager.Instance.OnInitialized(true);});
    }

    public void ShowBanner()
    {
        DestroyBanner();
   
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerView = new BannerView(bannerID, adaptiveSize, AdPosition.Bottom);

        _bannerView.OnBannerAdLoadFailed += ((sender) => AdEvents.Instance.OnBannerFailedToLoad());
        //load banner
        _bannerView.LoadAd(CreateAdRequest());
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    public void HideBanner()
    {
        
    }

    public void DestroyBanner()
    {
        if(_bannerView != null)
        {
            _bannerView.Destroy();
        }
    }

    public void LoadInterstatial()
    {
        if (_interstatialAd != null)
        {
            _interstatialAd.Destroy();
        }

        _interstatialAd = new InterstitialAd(interstatialID);
        _interstatialAd.OnAdFailedToLoad += ((sender,args) => {AdEvents.Instance.OnInterstatialFailedToLoad();});
        _interstatialAd.OnAdClosed += (sender,args) => AdEvents.Instance.OnInterstatialAdShown();

        _interstatialAd.LoadAd(CreateAdRequest());

       
    }

    public void LoadRewarded()
    {
       _rewardedAd = new RewardedAd(rewardedID);

       _rewardedAd.OnAdFailedToLoad += (sender,args) => AdEvents.Instance.OnRewardedFailedToLoad();
       _rewardedAd.OnUserEarnedReward += (sender,args) => AdEvents.Instance.OnRewardedClosed(true);
       _rewardedAd.OnAdClosed += (sender,args) => AdEvents.Instance.OnRewardedClosed(false);
       _rewardedAd.LoadAd(CreateAdRequest());

    }

    public void ShowInterstatial()
    {
        if (isInterstatialReady())
        {
            _interstatialAd.Show();
        }
        else
        {
            AdEvents.Instance.OnInterstatialAdShownFailed();
        }
    }

    public void ShowRewarded()
    {
        if (isRewardedReady())
        {
            _rewardedAd.Show();
        }
        else
         {
            AdEvents.Instance.OnRewardedFailedToLoad();
         }
    }

    public bool isInterstatialReady()
    {
        return _interstatialAd.CanShowAd();
    }

    public bool isRewardedReady()
    {
        return _interstatialAd.CanShowAd();
    }
}
#endif
