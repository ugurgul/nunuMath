using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }
    public Action<bool> rewardedSuccessFunction;

    IAdLibrary adLibrary;
    [HideInInspector]
    bool enableBanner = true;
    [HideInInspector]
    bool enableInterstatial = true;
    [HideInInspector]
    bool enableRewarded =true;

    private float lastAdShownTime;

    private float adsShownInterval;
    private bool isTestMode = false;

    int retryTimeOut = 10;





    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
#if ADS_UNITY
        adLibrary = new AdsUnity();
#endif
#if ADS_ADMOB
        adLibrary = new AdsAdmob();
#endif
        adLibrary.Initialize(isTestMode);
    }

    public void OnInitialized(bool result)
    {
        if (result)
        {
            if (enableBanner)
                ShowBanner();
            if (enableInterstatial)
                LoadInterstatial();
            if (enableRewarded)
                LoadRewarded();
        }
        else
        {
            //Hata olduysa tekrar dene
        }
    }

    #region Banner

    public void HideBanner()
    {
    }

    public void ShowBanner()
    {
        adLibrary.ShowBanner();
    }

    public void DestroyBanner()
    {
    }

    #endregion

    #region Interstatial

    public void LoadInterstatial()
    {
        adLibrary.LoadInterstatial();
    }

    public void ShowInterstatial()
    {
        if(isInterstatialReady() && enableInterstatial)
        {
            adLibrary.ShowInterstatial();

        }
    }

    public bool isInterstatialReady()
    {
        return adLibrary.isInterstatialReady();
    }

    #endregion

    #region Rewarded

    public void LoadRewarded()
    {
        adLibrary.LoadRewarded();
    }

    public void ShowRewarded(Action<bool> returnCall = null)
    {
        adLibrary.ShowRewarded();
        rewardedSuccessFunction = returnCall;
    }

    public bool isRewardedReady()
    {
        return true;
    }

    #endregion

    // Yeni bir reklam göstermek için yeterli zaman geçtimi
    public bool isEnoughTimeElapsed()
    {
        return true;
    }

    internal void OnRewardedClosed(bool isFinished)
    {
        rewardedSuccessFunction(isFinished);
        if(!adLibrary.autoCache)
        {
            LoadRewarded();

        }
    }

    public void OnInterstatialShown()
    {
        if(!adLibrary.autoCache)
        {
            LoadInterstatial();

        }
    }

    internal void OnRewardedFailedToLoad()
    {
        Invoke(nameof(adLibrary.LoadRewarded),retryTimeOut);
    }

    internal void OnInterstatialFailedToLoad()
    {
        Invoke(nameof(adLibrary.LoadInterstatial),retryTimeOut);
    }

    internal void OnBannerFailedToLoad()
    {
        Invoke(nameof(adLibrary.ShowBanner),retryTimeOut);
    }
}