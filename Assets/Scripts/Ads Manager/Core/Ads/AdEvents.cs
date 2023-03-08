   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdEvents : MonoBehaviour
{
     public static AdEvents Instance { get; private set; }


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
   //Rewarded yüklemesi başarısız olduğunda
   public void OnRewardedFailedToLoad()
   {
    AdManager.Instance.OnRewardedFailedToLoad();

   }
//Interstatial yüklemesi başarısız olduğunda
   public void OnInterstatialFailedToLoad()
   {
      AdManager.Instance.OnInterstatialFailedToLoad();
   }
   //Banner yüklemesi başarısız olduğunda
   public void OnBannerFailedToLoad()
   {
      AdManager.Instance.OnBannerFailedToLoad();
   }

   
   //Rewarded Eventleri
   public void OnRewardedClicked()
   {
      
   }

   public void OnRewardedClosed(bool isFinished)
   {
      AdManager.Instance.OnRewardedClosed(isFinished);
   }

   public void OnRewardedLoaded()
   {
      
   }
//Interstatial Eventleri
   public void OnInterstatialClicked()
   {
      
   }

   public void OnInterstatialAdClosed()
   {
      
   }

   public void OnInterstatialAdShownFailed()
   {
      
   }

   public void OnInterstatialAdShown()
   {
      AdManager.Instance.OnInterstatialShown();
   }
   
   //Banner eventleri
   public void OnBannerLoaded()
   {
      
   }

   public void OnBannerClick()
   {
      
   }

   public void BannerShown()
   {
      
   }
}
