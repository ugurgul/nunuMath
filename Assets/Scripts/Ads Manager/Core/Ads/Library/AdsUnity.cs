#if ADS_UNITY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using JetBrains.Annotations;
using Unity.Services.Mediation;
using System;

public class AdsUnity : IAdLibrary
{

    private string _androidGameId = "5195593", _iOSGameId = "5195592";
    string _androidInterstatialId = "Interstitial_Android", _iOSInterstatialId = "Interstitial_iOS";
    string _androidRewardedId = "Rewarded_Android", _iOSRewardedId = "Rewarded_iOS";
    string _androidBannerId ="Banner_Android", _iOSBannerId = "Banner_iOS";

    string _bannerAdId = null, _interstatialId = null, _rewardedId = null;
    string _gameId;


    private IBannerAd _bannerAd;
    private IInterstitialAd _interstitialAd;
    private IRewardedAd _rewardedAd;

    public bool autoCache { get; set; }

    public async void Initialize(bool isTestMode)
    {
        autoCache = false;
        SetIds();
        Debug.Log("Unity Ads Initialized");

        InitializationOptions options = new InitializationOptions();
        options.SetGameId(_gameId);
        try
        {
            await UnityServices.InitializeAsync(options);
            OnInitialized();
        }
        catch (Exception e)
        {
            
            OnInitializeError(e);
        }
        
    }

    private void OnInitializeError(Exception e)
    {
        AdManager.Instance.OnInitialized(false);
    }

    private void OnInitialized()
    {
        Debug.Log("Unity Ads Initialized");
        AdManager.Instance.OnInitialized(true);

    }

    private void SetIds()
    {
        switch(Application.platform)
        {
            case RuntimePlatform.Android:
            _gameId = _androidGameId;
            _bannerAdId = _androidBannerId;
            _interstatialId = _androidInterstatialId;
            _rewardedId = _androidRewardedId;
            break;

            case RuntimePlatform.IPhonePlayer:
            _gameId = _iOSGameId;
            _bannerAdId = _iOSBannerId;
            _interstatialId = _iOSInterstatialId;
            _rewardedId = _iOSRewardedId;
            break;
            //PC de test etmek için------------------------------------------------------------------------------
            case RuntimePlatform.WindowsEditor or RuntimePlatform.LinuxEditor or RuntimePlatform.OSXEditor:
            _gameId = _androidGameId;
            _bannerAdId = _androidBannerId;
            _interstatialId = _androidInterstatialId;
            _rewardedId = _androidRewardedId;
            break;

        }

    }

    public async void ShowBanner()
    {
        BannerAdAnchor bannerAdAnchor = BannerAdAnchor.BottomCenter;
        BannerAdSize bannerAdSize = BannerAdPredefinedSize.Banner.ToBannerAdSize();
        _bannerAd = MediationService.Instance.CreateBannerAd(_bannerAdId,bannerAdSize,bannerAdAnchor);
        try
        {
            await _bannerAd.LoadAsync();
            AdEvents.Instance.OnBannerLoaded();
        }
        catch (System.Exception)
        {
            
            AdEvents.Instance.OnBannerFailedToLoad();
        }
        


    }

    public void HideBanner()
    {
        
    }

    public void DestroyBanner()
    {
        
    }

    public async void LoadInterstatial()
    {
        _interstitialAd = MediationService.Instance.CreateInterstitialAd(_interstatialId);
        _interstitialAd.OnShowed += InterstitialAdOnOnShowed;
        try
        {
            await _interstitialAd.LoadAsync();
        }
        catch (System.Exception)
        {
            AdEvents.Instance.OnInterstatialFailedToLoad();

        }
    }

    private void InterstitialAdOnOnShowed(object sender, EventArgs e)
    {
        AdEvents.Instance.OnInterstatialAdShown();
    }

    public async void LoadRewarded()
    {
        _rewardedAd = MediationService.Instance.CreateRewardedAd(_rewardedId);
        _rewardedAd.OnShowed += RewardedAdOnOnShowed;
        try
        {
            await _rewardedAd.LoadAsync();
        }
        catch (System.Exception)
        {
            AdEvents.Instance.OnRewardedFailedToLoad();
        }
    }

    private void RewardedAdOnOnShowed(object sender, EventArgs e)
    {
        AdEvents.Instance.OnRewardedClosed(false);

    }

    public async void ShowInterstatial()
    {
        if (isInterstatialReady())
        {
            try
            {
                await _interstitialAd.ShowAsync();
            }
            catch (System.Exception)
            {
                
            }
        }
    }

    public async void ShowRewarded()
    {
        _rewardedAd.OnUserRewarded += RewardedAdOnOnUserRewarded;
        if(isRewardedReady())
        {
            try
            {
                await _rewardedAd.ShowAsync();
            }
            catch (System.Exception)
            {
                AdEvents.Instance.OnInterstatialAdShownFailed();
            }

        }
    }

    private void RewardedAdOnOnUserRewarded (object sender, RewardEventArgs e)
    {
        AdEvents.Instance.OnRewardedClosed(true);
    }



    public bool isInterstatialReady()
    {
        return _interstitialAd.AdState == AdState.Loaded;
    }

    public bool isRewardedReady()
    {
        return _rewardedAd.AdState == AdState.Loaded;
    }
}
#endif
