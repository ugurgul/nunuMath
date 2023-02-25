using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using TigerForge;
using DG.Tweening;
using UnityEngine.Localization.Settings;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject seviyePanel,mainMenuPanel,skorPanel,cikisPanel,skorResetPanel,kapatmaPanel,bulutlar_object,satinAlPanel;

    [SerializeField]
    private GameObject cetvel1,cetvel2,cetvel3;

    [SerializeField]
    private GameObject cetvel,kalem;

    [SerializeField]
    Button btnSkorKapat;

    [SerializeField]
     private Transform transition_Circle;

    public AudioSource audioSource;
    public AudioClip   audioClipClick,audioClipSetting,audioTransition,audioClosed;

    MainAnim mainAnim;
    AyarlarManager ayarlarManager;
    GameManager gameManager;
    AdController adController;
    public static MainMenuManager Instance;

    int toplamDogru, toplamYanlisAdet, toplamPuanAdet;
    public int toplamaD, toplamaY, toplamaP;
    public int cikartmaD, cikartmaY, cikartmaP;
    public int carpmaD, carpmaY, carpmaP;
    public int bolmeD, bolmeY, bolmeP;
    public int karisikD, karisikY, karisikP;
    int uyelikDurumu = 0;
    public bool acKapa;
    public bool acKapa_Cikarma;
    public bool acKapa_Carpma;
    public bool acKapa_Bolme;
    public bool acKapa_Karisik;

    string hangiIslemHangiSeviye;

    EasyFileSave myFile;
    public TMP_Text txtIslem;

    private void Awake() {
        mainAnim = Object.FindObjectOfType<MainAnim>();
        ayarlarManager = Object.FindObjectOfType<AyarlarManager>();
        gameManager = Object.FindObjectOfType<GameManager>();
        adController = Object.FindObjectOfType<AdController>();

        myFile = new EasyFileSave();

            if (Instance == null)
        {
            Instance = this;
            
        
        }

        //PlayerPrefs.DeleteKey("uyelikDurumu");


        if(PlayerPrefs.HasKey("uyelikDurumu"))
        {
            uyelikDurumu = PlayerPrefs.GetInt("uyelikDurumu",0);
        }

    }
    

    void Start()
    {
        Debug.Log("üyelik durumu :"+uyelikDurumu);

        audioSource = GetComponent<AudioSource>();

        DOTween.SetTweensCapacity(2000, 100);
        
        if(myFile.Load()){
        toplamPuanAdet = myFile.GetInt("toplamPuan");
        toplamDogru = myFile.GetInt("toplamDogru");
        toplamYanlisAdet = myFile.GetInt("toplamYanlis");

        toplamaD = myFile.GetInt("toplamaD");
        toplamaY = myFile.GetInt("toplamaY");
        toplamaP = myFile.GetInt("toplamaP");

        cikartmaD = myFile.GetInt("cikartmaD");
        cikartmaY = myFile.GetInt("cikartmaY");
        cikartmaP = myFile.GetInt("cikartmaP");

        carpmaD = myFile.GetInt("carpmaD");
        carpmaY = myFile.GetInt("carpmaY");
        carpmaP = myFile.GetInt("carpmaP");

        bolmeD = myFile.GetInt("bolmeD");
        bolmeY = myFile.GetInt("bolmeY");
        bolmeP = myFile.GetInt("bolmeP");

        karisikD = myFile.GetInt("karisikD");
        karisikY = myFile.GetInt("karisikY");
        karisikP = myFile.GetInt("karisikP");

        acKapa = myFile.GetBool("acKapa");
        acKapa_Cikarma = myFile.GetBool("acKapa_Cikarma");
        acKapa_Carpma = myFile.GetBool("acKapa_Carpma");
        acKapa_Bolme = myFile.GetBool("acKapa_Bolme");
        acKapa_Karisik = myFile.GetBool("acKapa_Karisik");


        }
        
    }

    void Update()
    {

    }

    //MAIN MENU BUTON FONKSİYONLARI
    //-------------------------------------------------------------------------------------------

    public void SoundControlBtn(AudioClip audioClip)
    {
           if (ayarlarManager.acKapaSes == false)
        {
            audioSource.Stop();
        }
        else
        {
             audioSource.clip = audioClip;  
             audioSource.Play();
        }

    }


    public void BtnTopla()
    {
        SoundControlBtn(audioClipClick);
        SeviyePaneliAc("toplama");
        kapatmaPanel.SetActive(true);

    }

    public void BtnCikart()
    {
        SoundControlBtn(audioClipClick);
        SeviyePaneliAc("cikartma");
        kapatmaPanel.SetActive(true);

    }

    public void BtnCarp()
    {
        SoundControlBtn(audioClipClick);
        SeviyePaneliAc("carpma");
        kapatmaPanel.SetActive(true);

    }

    public void BtnBol()
    {
        SoundControlBtn(audioClipClick);
        SeviyePaneliAc("bolme");
        kapatmaPanel.SetActive(true);

    }

    public void BtnKarisik()
    {
        SoundControlBtn(audioClipClick);
        SeviyePaneliAc("karisik");
        kapatmaPanel.SetActive(true);

    }




    //---------------------------------------------------------------------------------------------------


    void SeviyePaneliAc(string hangiIslem)
    {

        if (hangiIslem == "toplama")
        {
            txtIslem.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","toplama");
            hangiIslemHangiSeviye = hangiIslem;
        }
        else if (hangiIslem == "cikartma")
        {
            txtIslem.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","cikarma");
            hangiIslemHangiSeviye = hangiIslem;
        }
        else if (hangiIslem == "carpma")
        {
            txtIslem.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","carpma");
            hangiIslemHangiSeviye = hangiIslem;
        }
        else if (hangiIslem == "bolme")
        {
            txtIslem.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","bolme");
            hangiIslemHangiSeviye = hangiIslem;
        }
        else if (hangiIslem == "karisik")
        {
            txtIslem.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","karisik");
            hangiIslemHangiSeviye = hangiIslem;
        }

        seviyePanel.GetComponent<CanvasGroup>().DOFade(1,0.5f);
        seviyePanel.GetComponent<RectTransform>().DOScale(1,0.5f).SetEase(Ease.OutBack);
        seviyePanel.SetActive(true);
        PlayerPrefs.SetString("hangiIslem",hangiIslem);

        cetvel1.transform.DOShakeRotation(1,new Vector3(0,0,-22));
        cetvel2.transform.DOShakeRotation(1,new Vector3(0,0,25));
        cetvel3.transform.DOShakeRotation(1,new Vector3(0,0,35));
    }

    //SEVİYE PANELİ BUTON FONKSİYONLARI
    //------------------------------------------------------------------------------------------------------------
    public void BtnSeviyePaneliKapat()
    {
        audioSource.PlayOneShot(audioClosed);

        kapatmaPanel.SetActive(false);
        seviyePanel.GetComponent<CanvasGroup>().DOFade(0,0.5f);
        seviyePanel.GetComponent<RectTransform>().DOScale(0,0.5f).SetEase(Ease.OutBack);
    }


    public void HangiSeviyeAcilsin(string hangiSeviye)
    {
        
        SoundControlBtn(audioClipClick);

        PlayerPrefs.SetString("hangiSeviye",hangiSeviye);

        PlayerPrefs.Save();

        bulutlar_object.SetActive(false);

        audioSource.clip = audioTransition;
        transition_Circle.transform.DOLocalMoveY(3380f,0.8f).SetEase(Ease.OutQuint);

        cetvel.transform.DOShakeRotation(1,new Vector3(0,0,25));
        kalem.transform.DOShakeRotation(1,new Vector3(0,0,-20));
        audioSource.Play();

        StartCoroutine(LoadScene_Couritine());

    }
    
    
    IEnumerator LoadScene_Couritine()
   {
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(2);

   }


    public void BtnKolay()
    {
        SoundControlBtn(audioClipClick);
      HangiSeviyeAcilsin("kolay");

    }

    public void BtnOrta()
     {
        SoundControlBtn(audioClipClick);
         HangiSeviyeAcilsin("orta");

     }

    public void BtnZor()
     {
        SoundControlBtn(audioClipClick);
         HangiSeviyeAcilsin("zor");

     }
    //-------------------------------------------------------------------------------------------------------------


    public void BtnAnaSayfaSkor()
    {
        
        SoundControlBtn(audioClipSetting);

        skorPanel.GetComponent<CanvasGroup>().DOFade(1,0.5f);
        skorPanel.GetComponent<RectTransform>().DOScale(1,0.5f).SetEase(Ease.OutBack);
        kapatmaPanel.SetActive(true);
      


        GameObject.Find("txtToplamDogru").GetComponent<TMP_Text>().text = toplamDogru.ToString();
        GameObject.Find("txtToplamYanlis").GetComponent<TMP_Text>().text = toplamYanlisAdet.ToString();
        GameObject.Find("txtToplamPuan").GetComponent<TMP_Text>().text = toplamPuanAdet.ToString();

        GameObject.Find("txtToplamaD").GetComponent<TMP_Text>().text = toplamaD.ToString();
        GameObject.Find("txtToplamaY").GetComponent<TMP_Text>().text = toplamaY.ToString();
        GameObject.Find("txtToplamaP").GetComponent<TMP_Text>().text = toplamaP.ToString();

        GameObject.Find("txtCikartmaD").GetComponent<TMP_Text>().text = cikartmaD.ToString();
        GameObject.Find("txtCikartmaY").GetComponent<TMP_Text>().text = cikartmaY.ToString();
        GameObject.Find("txtCikartmaP").GetComponent<TMP_Text>().text = cikartmaP.ToString();

        GameObject.Find("txtCarpmaD").GetComponent<TMP_Text>().text = carpmaD.ToString();
        GameObject.Find("txtCarpmaY").GetComponent<TMP_Text>().text = carpmaY.ToString();
        GameObject.Find("txtCarpmaP").GetComponent<TMP_Text>().text = carpmaP.ToString();

        GameObject.Find("txtBolmeD").GetComponent<TMP_Text>().text = bolmeD.ToString();
        GameObject.Find("txtBolmeY").GetComponent<TMP_Text>().text = bolmeY.ToString();
        GameObject.Find("txtBolmeP").GetComponent<TMP_Text>().text = bolmeP.ToString();

        GameObject.Find("txtKarisikD").GetComponent<TMP_Text>().text = karisikD.ToString();
        GameObject.Find("txtKarisikY").GetComponent<TMP_Text>().text = karisikY.ToString();
        GameObject.Find("txtKarisikP").GetComponent<TMP_Text>().text = karisikP.ToString();
    }
    public void BtnAnaSayfaSkorKapat()
    {
        audioSource.PlayOneShot(audioClosed);
        kapatmaPanel.SetActive(false);
        skorPanel.GetComponent<CanvasGroup>().DOFade(0,0.5f);
        skorPanel.GetComponent<RectTransform>().DOScale(0,0.5f).SetEase(Ease.OutBack);
    }
    public void BtnCikis()
    {
        cikisPanel.GetComponent<CanvasGroup>().DOFade(1,0.5f);
        cikisPanel.GetComponent<RectTransform>().DOScale(1,0.5f).SetEase(Ease.OutBack);

        SoundControlBtn(audioClipSetting);
        kapatmaPanel.SetActive(true);

    }

    public void BtnCikisPanelKapat()
    {

        audioSource.PlayOneShot(audioClosed);
  
        kapatmaPanel.SetActive(false);
        cikisPanel.GetComponent<CanvasGroup>().DOFade(0,0.5f);
        cikisPanel.GetComponent<RectTransform>().DOScale(0,0.5f).SetEase(Ease.OutBack);
    }

    public void BtnCikisEvet()
    {
        Application.Quit();
    }

    public void BtnSkorResetPanel()
    {
        
        SoundControlBtn(audioClipSetting);
        skorResetPanel.SetActive(true);
        btnSkorKapat.enabled = false;

    }
    public void BtnSkorResetPanelKapat()
    {
        audioSource.PlayOneShot(audioClosed);
        skorResetPanel.SetActive(false);
        btnSkorKapat.enabled = true;
    }

    public void BtnEvetReset()
    {
     toplamDogru = 0;
     toplamYanlisAdet = 0;
     toplamPuanAdet = 0;
     toplamaD = 0;
     toplamaY = 0;
     toplamaP = 0;;
     cikartmaD = 0;
     cikartmaY = 0;
     cikartmaP = 0;
     carpmaD = 0;
     carpmaY = 0;
     carpmaP = 0;
     bolmeD = 0;
     bolmeY = 0;
     bolmeP = 0;
     karisikD = 0;
     karisikY = 0;
     karisikP = 0;
     acKapa = true;
     acKapa_Cikarma = true;
     acKapa_Carpma = true;
     acKapa_Bolme = true;
     acKapa_Karisik = true;
        GameObject.Find("txtToplamDogru").GetComponent<TMP_Text>().text = toplamDogru.ToString();
        GameObject.Find("txtToplamYanlis").GetComponent<TMP_Text>().text = toplamYanlisAdet.ToString();
        GameObject.Find("txtToplamPuan").GetComponent<TMP_Text>().text = toplamPuanAdet.ToString();

        GameObject.Find("txtToplamaD").GetComponent<TMP_Text>().text = toplamaD.ToString();
        GameObject.Find("txtToplamaY").GetComponent<TMP_Text>().text = toplamaY.ToString();
        GameObject.Find("txtToplamaP").GetComponent<TMP_Text>().text = toplamaP.ToString();

        GameObject.Find("txtCikartmaD").GetComponent<TMP_Text>().text = cikartmaD.ToString();
        GameObject.Find("txtCikartmaY").GetComponent<TMP_Text>().text = cikartmaY.ToString();
        GameObject.Find("txtCikartmaP").GetComponent<TMP_Text>().text = cikartmaP.ToString();

        GameObject.Find("txtCarpmaD").GetComponent<TMP_Text>().text = carpmaD.ToString();
        GameObject.Find("txtCarpmaY").GetComponent<TMP_Text>().text = carpmaY.ToString();
        GameObject.Find("txtCarpmaP").GetComponent<TMP_Text>().text = carpmaP.ToString();

        GameObject.Find("txtBolmeD").GetComponent<TMP_Text>().text = bolmeD.ToString();
        GameObject.Find("txtBolmeY").GetComponent<TMP_Text>().text = bolmeY.ToString();
        GameObject.Find("txtBolmeP").GetComponent<TMP_Text>().text = bolmeP.ToString();

        GameObject.Find("txtKarisikD").GetComponent<TMP_Text>().text = karisikD.ToString();
        GameObject.Find("txtKarisikY").GetComponent<TMP_Text>().text = karisikY.ToString();
        GameObject.Find("txtKarisikP").GetComponent<TMP_Text>().text = karisikP.ToString();

            myFile.Add("toplamPuan",toplamPuanAdet);
            myFile.Add("toplamDogru",toplamDogru);
            myFile.Add("toplamYanlis",toplamYanlisAdet);

            myFile.Add("toplamaD",toplamaD);
            myFile.Add("toplamaY",toplamaY);
            myFile.Add("toplamaP",toplamaP);

            myFile.Add("cikartmaD",cikartmaD);
            myFile.Add("cikartmaY",cikartmaY);
            myFile.Add("cikartmaP",cikartmaP);

            myFile.Add("carpmaD",carpmaD);
            myFile.Add("carpmaY",carpmaY);
            myFile.Add("carpmaP",carpmaP);

            myFile.Add("bolmeD",bolmeD);
            myFile.Add("bolmeY",bolmeY);
            myFile.Add("bolmeP",bolmeP);

            myFile.Add("karisikD",karisikD);
            myFile.Add("karisikY",karisikY);
            myFile.Add("karisikP",karisikP);
            myFile.Add("acKapa",acKapa);
            myFile.Add("acKapa_Cikarma",acKapa_Cikarma);
            myFile.Add("acKapa_Carpma",acKapa_Carpma);
            myFile.Add("acKapa_Bolme",acKapa_Bolme);
            myFile.Add("acKapa_Karisik",acKapa_Karisik);

            myFile.Save();

    
     
     skorResetPanel.SetActive(false);
     btnSkorKapat.enabled = true;
    }

    public void SatinAlPanelAc()
    {
        SoundControlBtn(audioClipSetting);
        satinAlPanel.GetComponent<CanvasGroup>().DOFade(1,0.5f);
        satinAlPanel.GetComponent<RectTransform>().DOScale(1,0.5f).SetEase(Ease.OutBack);
        kapatmaPanel.SetActive(true);
    }

    public void SatinAlPanelKapat()
    {
        audioSource.PlayOneShot(audioClosed);
        satinAlPanel.GetComponent<CanvasGroup>().DOFade(0,0.5f);
        satinAlPanel.GetComponent<RectTransform>().DOScale(0,0.5f).SetEase(Ease.OutBack);
        kapatmaPanel.SetActive(false);
    }




}
