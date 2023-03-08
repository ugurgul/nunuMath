using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdLibrary
{
   bool autoCache{get; set;}
   void Initialize(bool isTestMode);
   void ShowBanner();
   void HideBanner();
   void DestroyBanner();


   void LoadInterstatial();
   void LoadRewarded();

   void ShowInterstatial();
   void ShowRewarded();

   bool isInterstatialReady();
   bool isRewardedReady();
}
