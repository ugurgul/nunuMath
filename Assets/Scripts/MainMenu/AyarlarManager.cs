using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Notifications.Android;

public class AyarlarManager : MonoBehaviour
{
    [SerializeField]
    GameObject ayarlarPanel,kapatmaPanel;

    [SerializeField]
    GameObject a1,a2,a3;

    [SerializeField]
    public RectTransform toggleSes,toggleMuzik,toggleBildirimler;
    
    [SerializeField]
    public Color backActiveColor,toggleActiveColor;

    public Color backDefaultColor,toggleDefaultColor;

    [SerializeField]
    public Image backImage,toggleImage,backImageMuzik,toggleImageMuzik,toggleImageBildirimler,backImageBildirimler;

    MainMenuManager mainMenuManager;

    Vector2 handlePosition;

    public bool acKapaSes = false;
    public bool acKapaMuzik = false;
    public bool acKapaBildirimler = false;

    public AudioSource audioSourceAyarlar;

    [SerializeField]
    AudioClip audioClosed;



    
    private void Awake() {

        handlePosition = toggleSes.anchoredPosition;
        backDefaultColor = backImage.color;
        toggleDefaultColor = toggleImage.color;

    }

    private void Start() {
        mainMenuManager = Object.FindObjectOfType<MainMenuManager>();
        audioSourceAyarlar = GetComponent<AudioSource>();

 
        if(PlayerPrefs.HasKey("muzik"))
        {
            acKapaMuzik = (PlayerPrefs.GetInt("muzik") != 0);

            if (acKapaMuzik == true)
            {
                muzikToggleSag();
            
                audioSourceAyarlar.Play();
                Debug.Log("müzik true girdi");

            }
            else
            {
                muzikToggleSol();

                audioSourceAyarlar.Stop();
              
            }

        
        }


         if (PlayerPrefs.HasKey("ses"))
        {
            acKapaSes = (PlayerPrefs.GetInt("ses") != 0);

            if (acKapaSes == true)
            {
                sesToggleSag();

                mainMenuManager.audioSource.Play();

            }
            else
            {
                sesToggleSol();

                mainMenuManager.audioSource.Stop();

            }

        }

        if (PlayerPrefs.HasKey("bildirimler"))
        {
            acKapaBildirimler = (PlayerPrefs.GetInt("bildirimler") != 0);

            if (acKapaBildirimler == true)
            {
                bildirimlerToggleSag();

                Debug.Log("bildirimler true girdi");

            }
            else
            {
                bildirimlerToggleSol();

            }

        }


        if (PlayerPrefs.HasKey("ses") == false && PlayerPrefs.HasKey("muzik") == false && PlayerPrefs.HasKey("bildirimler") == false)
        {

            muzikToggleSag();
            sesToggleSag();
            bildirimlerToggleSag();

            audioSourceAyarlar.Play();
            acKapaMuzik = true;
            PlayerPrefs.SetInt("muzik", (acKapaMuzik ? 1 : 0));
            
            acKapaSes = true;
            PlayerPrefs.SetInt("ses", (acKapaSes ? 1 : 0));

            acKapaBildirimler = true;
            PlayerPrefs.SetInt("bildirimler",(acKapaBildirimler ? 1 : 0));

            PlayerPrefs.Save();

        }




    }

    private void OnApplicationPause(bool pauseStatus) {

        

        if (acKapaBildirimler == true)
        {
             Send_Notification();

             Debug.Log("bildirim gönderildi");
        }
        else
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }


       
    }


    public void Send_Notification()
    {


        AndroidNotificationCenter.CancelAllDisplayedNotifications();


          var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
         };
         AndroidNotificationCenter.RegisterNotificationChannel(channel);

         var notification = new AndroidNotification();
         notification.Title = "Hey! How about doing a Mathematics Exercise?";
         notification.Text = "Come back and let's play together :)";
         notification.FireTime = System.DateTime.Now.AddHours(48);

         var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

         if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
         {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
         }

    }

    

    public void OnSwitch() {

        Debug.Log(acKapaSes);

           if(acKapaSes == false) 
        {
            sesToggleSag();

            mainMenuManager.audioSource.Play();
            
            acKapaSes = true;
            PlayerPrefs.SetInt("ses", (acKapaSes ? 1 : 0));
    

        }

        else if (acKapaSes == true)
        {
            sesToggleSol();

            mainMenuManager.audioSource.Stop();

            acKapaSes = false;
            PlayerPrefs.SetInt("ses", (acKapaSes ? 1 : 0));
      

        }
       
    }
    /* ----------------------------------------------------------------------------------*/

    public void OnSwitchMuzik() {
  

        if(acKapaMuzik == false) 
        {
            muzikToggleSag();

            if(!audioSourceAyarlar.isPlaying)
            {
              audioSourceAyarlar.Play();
            }
            
            acKapaMuzik = true;
            PlayerPrefs.SetInt("muzik", (acKapaMuzik ? 1 : 0));
    
        }

        else if (acKapaMuzik == true)
        {
            muzikToggleSol();

            if(audioSourceAyarlar.isPlaying)
            {
              audioSourceAyarlar.Stop();
            }

            acKapaMuzik = false;
            PlayerPrefs.SetInt("muzik", (acKapaMuzik ? 1 : 0));

        }

    }

    public void OnSwitcBildirimler() {
        if(acKapaBildirimler == false) 
        {
            bildirimlerToggleSag();
   
            acKapaBildirimler = true;
            PlayerPrefs.SetInt("bildirimler", (acKapaBildirimler ? 1 : 0));
        }

        else if (acKapaBildirimler == true)
        {

            bildirimlerToggleSol();

             acKapaBildirimler = false;
             PlayerPrefs.SetInt("bildirimler", (acKapaBildirimler ? 1 : 0));


        }

    }

    void muzikToggleSol()
    {
        toggleMuzik.DOAnchorPos( handlePosition,0.4f).SetEase(Ease.InOutBack);
        backImageMuzik.DOColor(backDefaultColor,0.6f);
        toggleImageMuzik.DOColor(toggleDefaultColor,0.6f);

    }
    void muzikToggleSag()
    {
        toggleMuzik.DOAnchorPos( handlePosition * (-68),0.4f).SetEase(Ease.InOutBack);
        backImageMuzik.DOColor(backActiveColor,0.6f);
        toggleImageMuzik.DOColor(toggleActiveColor,0.6f);
        
    }
    void sesToggleSol()
    {
        toggleSes.DOAnchorPos( handlePosition,0.4f).SetEase(Ease.InOutBack);
        backImage.DOColor(backDefaultColor,0.6f);
        toggleImage.DOColor(toggleDefaultColor,0.6f);

    }
    void sesToggleSag()
    {
        toggleSes.DOAnchorPos( handlePosition * (-68),0.4f).SetEase(Ease.InOutBack);
        backImage.DOColor(backActiveColor,0.6f);
        toggleImage.DOColor(toggleActiveColor,0.6f);
        
    }
    void bildirimlerToggleSag()
    {
            toggleBildirimler.DOAnchorPos( handlePosition * (-68),0.4f).SetEase(Ease.InOutBack);
            backImageBildirimler.DOColor(backActiveColor,0.6f);
            toggleImageBildirimler.DOColor(toggleActiveColor,0.6f);

    }
    void bildirimlerToggleSol()
    {
            toggleBildirimler.DOAnchorPos( handlePosition,0.4f).SetEase(Ease.InOutBack);

            backImageBildirimler.DOColor(backDefaultColor,0.6f);
            toggleImageBildirimler.DOColor(toggleDefaultColor,0.6f);

    }


    private void OnDestroy() {
          PlayerPrefs.Save();
          
    }

    public void BtnAyarlarKapat()
    {
        //ayarlarPanel.SetActive(false);
        audioSourceAyarlar.PlayOneShot(audioClosed);
        kapatmaPanel.SetActive(false);
        ayarlarPanel.GetComponent<CanvasGroup>().DOFade(0,0.5f);
        ayarlarPanel.GetComponent<RectTransform>().DOScale(0,0.5f).SetEase(Ease.OutBack); 



    }

    public void BtnAyarlarAc()
    {
        if (acKapaSes == false)
        {
            mainMenuManager.audioSource.Stop();
        }
        else
        {
             mainMenuManager.audioSource.clip = mainMenuManager.audioClipSetting;  
             mainMenuManager.audioSource.Play();
        }

        ayarlarPanel.GetComponent<CanvasGroup>().DOFade(1,0.5f);
        ayarlarPanel.GetComponent<RectTransform>().DOScale(1,0.5f).SetEase(Ease.OutBack);
    
        a1.transform.DOShakeRotation(1,new Vector3(0,0,25));
        a2.transform.DOShakeRotation(1,new Vector3(0,0,-20));
        a3.transform.DOShakeRotation(1,new Vector3(0,0,-25));
        //ayarlarPanel.SetActive(true);
        kapatmaPanel.SetActive(true);


        
    }



}
