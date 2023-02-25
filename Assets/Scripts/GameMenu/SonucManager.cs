using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using TigerForge;
using DG.Tweening;
using UnityEngine.Localization.Settings;

public class SonucManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sonucPanel,sonucImg,backImg,star,star2,star3;

    GameManager gameManager;
    AdController adController;

    [SerializeField]
     private Transform transition_Circle;

    [SerializeField]
    private GameObject cetvel,kalem;

    [SerializeField]
    Sprite[] sonucBack;

    public AudioSource audioSource;
    public AudioClip audioFinish,audioTransition;

    EasyFileSave myFile;

    bool kontrol = false;

    public int speed = 30;

    private void Awake() {
        myFile = new EasyFileSave();
        gameManager = Object.FindObjectOfType<GameManager>();
        adController = Object.FindObjectOfType<AdController>();
    }



    void Start()
    {
        SonucPanelAcKapa(kontrol);
        audioSource = GetComponent<AudioSource>();

        sonucImg.GetComponent<Image>().sprite = sonucBack[0];


    }

    private void Update() {
        sonucImg.gameObject.transform.Rotate(Vector3.forward*speed*Time.deltaTime);
    }

    public void SonucPanelAcKapa(bool acKapa)
    {
        kontrol = acKapa;

        if ( kontrol == true )
        {
        gameManager.siklarPanel.SetActive(false);
        gameManager.soruPanel.SetActive(false);
        //sonucPanel.SetActive(true);
        backImg.SetActive(false);
        audioSource.PlayOneShot(audioFinish);

        
        LeanTween.scale(sonucPanel,new Vector3(1f,1f,1f),1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star,new Vector3(1f,1f,1f),1.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star2,new Vector3(1f,1f,1f),1.5f).setDelay(0.8f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star3,new Vector3(1f,1f,1f),1.5f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);
        
        
        GameObject.Find("txtDogruAdet").GetComponent<TMP_Text>().text = gameManager.dogruAdet.ToString();
        GameObject.Find("txtYanlisAdet").GetComponent<TMP_Text>().text =  gameManager.yanlisAdet.ToString();
        GameObject.Find("txtPuanSonuc").GetComponent<TMP_Text>().text =  gameManager.puan.ToString();
        
            if (gameManager.hangiIslem == "toplama")
            {
                GameObject.Find("txtIslem").GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","toplama");

            }
            else if (gameManager.hangiIslem == "cikartma")
            {
                GameObject.Find("txtIslem").GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","cikarma");

            }
            else if (gameManager.hangiIslem == "carpma")
            {
                 GameObject.Find("txtIslem").GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","carpma");

            }
            else if (gameManager.hangiIslem == "bolme")
            {
                GameObject.Find("txtIslem").GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","bolme");

            }
            else if (gameManager.hangiIslem == "karisik")
            {
                GameObject.Find("txtIslem").GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","karisik");

            }

        }
        else
        {
            gameManager.siklarPanel.SetActive(true);
            gameManager.soruPanel.SetActive(true);
            //sonucPanel.SetActive(false);
             LeanTween.scale(sonucPanel,new Vector3(0,0,0),0).setEase(LeanTweenType.easeOutElastic);
        }
    }




    public IEnumerator LoadScene_Couritine(int scene)
   {
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(scene);

   }

    public void BtnAnaSayfa()
    {
        adController.bannerView.Hide();
        audioSource.clip = audioTransition;
        transition_Circle.transform.DOLocalMoveY(3380f,0.8f).SetEase(Ease.OutQuint);

        cetvel.transform.DOShakeRotation(1,new Vector3(0,0,25));
        kalem.transform.DOShakeRotation(1,new Vector3(0,0,-20));
        audioSource.Play();
        StartCoroutine(LoadScene_Couritine(1));
    }

    public void BtnTekrarOyna()
    {
        adController.bannerView.Hide();
        sonucPanel.SetActive(false);
        audioSource.clip = audioTransition;
        transition_Circle.transform.DOLocalMoveY(3380f,0.8f).SetEase(Ease.OutQuint);

        cetvel.transform.DOShakeRotation(1,new Vector3(0,0,25));
        kalem.transform.DOShakeRotation(1,new Vector3(0,0,-20));
        audioSource.Play();
        StartCoroutine(LoadScene_Couritine(2));


    }


}
