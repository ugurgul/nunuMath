using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using TigerForge;
using DG.Tweening;
using UnityEngine.Localization.Settings;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject kapatmaPanel,cikisPanel,btnA_Panel,btnB_Panel,btnC_Panel,btnD_Panel;

    [SerializeField]
    private Text txtSoru,txtSoru_1,txtSoru_2,txt_Bolen,txt_Bolunen,txt_KacinciSoru,txt_Puan;

    [SerializeField]
    private Button btnA, btnB, btnC, btnD;

    [SerializeField]
    Sprite imgAktif_A,imgPasif_A,imgPasif_D,imgDogruBtn;

    [SerializeField]
    private Sprite[] rozetler;

    public GameObject siklarPanel, soruPanel,siklarPanel_KapaAc,toplama_Panel,cikarma_Panel,carpma_Panel,bolme_Panel,kararmaUstPanel;

    SonucManager sonucManager;
    public AudioSource audioSource;
    public AudioClip audioCorrect,audioFalse,audioTransition,audioRozet;

    EasyFileSave myFile;

    [SerializeField]
     private Transform transition_Circle,rozet_basari;

    [SerializeField]
    private Image rozetler_img;

    [SerializeField]
    private GameObject cetvel,kalem;

    public static GameManager Instance;
    public string hangiIslem,hangiSeviye;

    int sayi1, sayi2;
    int dogruSonuc, yanlisSonuc1, yanlisSonuc2, yanlisSonuc3;
    int rastgeleDeger,rand;
    string butonClickCheck;
    public int dogruAdet, yanlisAdet, puan;
    int toplamDogruAdet, toplamYanlisAdet, toplamPuan;
    int toplamaD, toplamaY, toplamaP;
    int cikartmaD, cikartmaY, cikartmaP;
    int carpmaD, carpmaY, carpmaP;
    int bolmeD, bolmeY, bolmeP;
    int karisikD, karisikY, karisikP;
    int kacinciSoru;
    int uyelikDurumu = 0;
    

    public bool acKapa = true;
    public bool acKapa_Cikarma = true;
    public bool acKapa_Carpma = true;
    public bool acKapa_Bolme = true;
    public bool acKapa_Karisik = true;
    public bool acKapaSes;

    Vector3 position,position2;

    MainMenuManager mainMenuManager;
    AdController adController;
    public TMP_Text txtRozet;



    private void Awake() {

        sonucManager = Object.FindObjectOfType<SonucManager>();
        mainMenuManager = Object.FindObjectOfType<MainMenuManager>();
        adController = Object.FindObjectOfType<AdController>();
        audioSource = GetComponent<AudioSource>();
        //PlayerPrefs.DeleteKey("uyelikDurumu");

         if(PlayerPrefs.HasKey("hangiIslem")){

            hangiIslem = PlayerPrefs.GetString("hangiIslem");
        }
        if(PlayerPrefs.HasKey("hangiSeviye")){

            hangiSeviye = PlayerPrefs.GetString("hangiSeviye");
        }
        if(PlayerPrefs.HasKey("uyelikDurumu"))
        {
            uyelikDurumu = PlayerPrefs.GetInt("uyelikDurumu",0);
            
        }
        
        
        if (Instance == null)
        {
            Instance = this;
            myFile = new EasyFileSave();
            LoadData();
            
        }
        else{
            Destroy(gameObject);
        }

          if (PlayerPrefs.HasKey("ses"))
        {
            acKapaSes = (PlayerPrefs.GetInt("ses") != 0);

            if (acKapaSes == false)
            {
                Debug.Log("audio girdi");
            
                audioSource.Stop();
            }

        }
        //DontDestroyOnLoad(gameObject);
        toplamPuan = 0;

    }

    void Start()
    {
        //uyelikDurumu =0;

        if(uyelikDurumu == 0)
        {
           adController.ShowBanner();
        }
 
       position = new Vector3(txtSoru_2.transform.position.x,txtSoru_2.transform.position.y, txtSoru_2.transform.position.z);
       Debug.Log(position);
       SoruYaz();
       audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if(dogruAdet == 10)
        {
            if (uyelikDurumu == 0)
             {
                adController.ShowInterstitial();
             }
            
            sonucManager.SonucPanelAcKapa(true);
            adController.bannerView.Hide();
            SaveData();
            Destroy(gameObject);
            dogruAdet = 0;
        }


        if(hangiIslem == "toplama")
        {
            if(toplamaD == 1 && acKapa == true)
            {
                Rozet_Move(0);
                acKapa = false;

            }
            else if (toplamaD == 10 && acKapa == false)
            {
                Rozet_Move(1);
                acKapa = true;
            }
            else if (toplamaD == 100 && acKapa == true)
            {
                Rozet_Move(2);
                acKapa = false;
            }
            else if (toplamaD == 500 && acKapa == false)
            {
                Rozet_Move(3);
                acKapa = true;
            }
            else if (toplamaD == 1000 && acKapa == true)
            {
                Rozet_Move(4);
                acKapa = false;
            }
            
        }
        else if (hangiIslem == "cikartma")
        {
           
            if(cikartmaD == 1 && acKapa_Cikarma == true)
            {
               
                Rozet_Move(5);
                acKapa_Cikarma = false;

            }
            else if (cikartmaD == 10 && acKapa_Cikarma == false)
            {
            
                Rozet_Move(6);
                acKapa_Cikarma = true;
            }
            else if (cikartmaD == 100 && acKapa_Cikarma == true)
            {
                Rozet_Move(7);
                acKapa_Cikarma = false;
            }
            else if (cikartmaD == 500 && acKapa_Cikarma == false)
            {
                Rozet_Move(8);
                acKapa_Cikarma = true;
            }
            else if (cikartmaD == 1000 && acKapa_Cikarma == true)
            {
                Rozet_Move(9);
                acKapa_Cikarma = false;
            }
        }
        else if (hangiIslem == "carpma")
        {
            if(carpmaD == 1 && acKapa_Carpma == true)
            {
                Rozet_Move(10);
                acKapa_Carpma = false;

            }
            else if (carpmaD == 10 && acKapa_Carpma == false)
            {
                Rozet_Move(11);
                acKapa_Carpma = true;
            }
            else if (carpmaD == 100 && acKapa_Carpma == true)
            {
                Rozet_Move(12);
                acKapa_Carpma = false;
            }
            else if (carpmaD == 500 && acKapa_Carpma == false)
            {
                Rozet_Move(13);
                acKapa_Carpma = true;
            }
            else if (carpmaD == 1000 && acKapa_Carpma == true)
            {
                Rozet_Move(14);
                acKapa_Carpma = false;
            }
        }
        else if (hangiIslem == "bolme")
        {
            if(bolmeD == 1 && acKapa_Bolme == true)
            {
                Rozet_Move(15);
                acKapa_Bolme = false;

            }
            else if (bolmeD == 10 && acKapa_Bolme == false)
            {
                Rozet_Move(16);
                acKapa_Bolme = true;
            }
            else if (bolmeD == 100 && acKapa_Bolme == true)
            {
                Rozet_Move(17);
                acKapa_Bolme = false;
            }
            else if (bolmeD == 500 && acKapa_Bolme == false)
            {
                Rozet_Move(18);
                acKapa_Bolme = true;
            }
            else if (bolmeD == 1000 && acKapa_Bolme == true)
            {
                Rozet_Move(19);
                acKapa_Bolme = false;
            }
        }
        else if (hangiIslem == "karisik")
        {
            if(karisikD == 1 && acKapa_Karisik == true)
            {
                Rozet_Move(20);
                acKapa_Karisik = false;

            }
            else if (karisikD == 10 && acKapa_Karisik == false)
            {
                Rozet_Move(21);
                acKapa_Karisik = true;
            }
            else if (karisikD == 100 && acKapa_Karisik == true)
            {
                Rozet_Move(22);
                acKapa_Karisik = false;
            }
            else if (karisikD == 500 && acKapa_Karisik == false)
            {
                Rozet_Move(23);
                acKapa_Karisik = true;
            }
            else if (karisikD == 1000 && acKapa_Karisik == true)
            {
                Rozet_Move(24);
                acKapa_Karisik = false;
            }
        }

    }

    private void OnDestroy() {
        SaveData();
    }

    void BtnReset()
    {
        btnA.GetComponent<Image>().sprite = imgPasif_A;
        btnB.GetComponent<Image>().sprite = imgPasif_A;
        btnC.GetComponent<Image>().sprite = imgPasif_A;
        btnD.GetComponent<Image>().sprite = imgPasif_A;

        btnA_Panel.SetActive(false);
        btnB_Panel.SetActive(false);
        btnC_Panel.SetActive(false);
        btnD_Panel.SetActive(false);
    }

    void SoruYaz()
    {

        BtnReset();
        kacinciSoru ++;
        txt_KacinciSoru.text = kacinciSoru.ToString();

        if(hangiIslem == "toplama" && hangiSeviye == "kolay")
        {
            ToplamaKolay();
            SiklariYaz();
        }
          else if (hangiIslem == "toplama" && hangiSeviye == "orta")
        {
            ToplamaOrta();
            SiklariYaz();
        }
          else if (hangiIslem == "toplama" && hangiSeviye == "zor")
        {
            ToplamaZor();
            SiklariYaz();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "cikartma" && hangiSeviye == "kolay")
        {
            CikartmaKolay();
            SiklariYaz();
        }
          else if (hangiIslem == "cikartma" && hangiSeviye == "orta")
        {
            CikartmaOrta();
            SiklariYaz();
        }
          else if (hangiIslem == "cikartma" && hangiSeviye == "zor")
        {
            CikartmaZor();
            SiklariYaz();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "carpma" && hangiSeviye == "kolay")
        {
            CarpmaKolay();
            SiklariYaz();
        }
          else if (hangiIslem == "carpma" && hangiSeviye == "orta")
        {
            CarpmaOrta();
            SiklariYaz();
        }
          else if (hangiIslem == "carpma" && hangiSeviye == "zor")
        {
            CarpmaZor();
            SiklariYaz();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "bolme" && hangiSeviye == "kolay")
        {
            BolmeKolay();
            SiklariYaz();
        } 
          else if (hangiIslem == "bolme" && hangiSeviye == "orta")
        {
            BolmeOrta();
            SiklariYaz();
        }
          else if (hangiIslem == "bolme" && hangiSeviye == "zor")
        {
            BolmeZor();
            SiklariYaz();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "karisik" && hangiSeviye == "kolay")
        {
             rastgeleDeger = Random.Range(1,121);

            if(rastgeleDeger < 31)
            {
                ToplamaKolay();
                SiklariYaz();

            }
            else if (rastgeleDeger < 61)
            {
                CikartmaKolay();
                SiklariYaz();

            }
            else if (rastgeleDeger < 91)
            {
                CarpmaKolay();
                SiklariYaz();

            }
            else
            {
                BolmeKolay();
                SiklariYaz();

            }
        }
          else if (hangiIslem == "karisik" && hangiSeviye == "orta")
        {
            rastgeleDeger = Random.Range(1,121);

            if(rastgeleDeger < 31)
            {
                ToplamaOrta();
                SiklariYaz();

            }
            else if (rastgeleDeger < 61)
            {
                CikartmaOrta();
                SiklariYaz();

            }
            else if (rastgeleDeger < 91)
            {
                CarpmaOrta();
                SiklariYaz();

            }
            else
            {
                BolmeOrta();
                SiklariYaz();

            }
        }
          else if (hangiIslem == "karisik" && hangiSeviye == "zor")
        {
            rastgeleDeger = Random.Range(1,121);

            if(rastgeleDeger < 31)
            {
                ToplamaZor();
                SiklariYaz();

            }
            else if (rastgeleDeger < 61)
            {
                CikartmaZor();
                SiklariYaz();

            }
            else if (rastgeleDeger < 91)
            {
                CarpmaZor();
                SiklariYaz();

            }
            else
            {
                BolmeZor();
                SiklariYaz();

            }
        }
    }

    void SiklariYaz()
    {
        int randomValue = Random.Range(1,121);

        if(randomValue <= 30)
        {
            GameObject.Find("btnA").GetComponentInChildren<TMP_Text>().text = dogruSonuc.ToString();
            GameObject.Find("btnB").GetComponentInChildren<TMP_Text>().text = yanlisSonuc1.ToString();
            GameObject.Find("btnC").GetComponentInChildren<TMP_Text>().text = yanlisSonuc2.ToString();
            GameObject.Find("btnD").GetComponentInChildren<TMP_Text>().text = yanlisSonuc3.ToString();
            butonClickCheck = "a";

            

        }
        else if (randomValue < 60)
        {

            GameObject.Find("btnB").GetComponentInChildren<TMP_Text>().text = dogruSonuc.ToString();
            GameObject.Find("btnA").GetComponentInChildren<TMP_Text>().text = yanlisSonuc1.ToString();
            GameObject.Find("btnC").GetComponentInChildren<TMP_Text>().text = yanlisSonuc2.ToString();
            GameObject.Find("btnD").GetComponentInChildren<TMP_Text>().text = yanlisSonuc3.ToString();
            butonClickCheck = "b";

        }
        else if (randomValue < 90)
        {

            GameObject.Find("btnC").GetComponentInChildren<TMP_Text>().text = dogruSonuc.ToString();
            GameObject.Find("btnB").GetComponentInChildren<TMP_Text>().text = yanlisSonuc1.ToString();
            GameObject.Find("btnA").GetComponentInChildren<TMP_Text>().text = yanlisSonuc2.ToString();
            GameObject.Find("btnD").GetComponentInChildren<TMP_Text>().text = yanlisSonuc3.ToString();
            butonClickCheck = "c";

        }
        else
        {

            GameObject.Find("btnD").GetComponentInChildren<TMP_Text>().text = dogruSonuc.ToString();
            GameObject.Find("btnB").GetComponentInChildren<TMP_Text>().text = yanlisSonuc1.ToString();
            GameObject.Find("btnC").GetComponentInChildren<TMP_Text>().text = yanlisSonuc2.ToString();
            GameObject.Find("btnA").GetComponentInChildren<TMP_Text>().text = yanlisSonuc3.ToString();
            butonClickCheck = "d";

        }
    }
    
    void ToplamaKolay()
    {
            sayi1 = Random.Range(1,10);
            sayi2 = Random.Range(1,10);
            txtSoru.text = sayi1.ToString() + " + " + sayi2.ToString() + " = ?";

            dogruSonuc = sayi1 + sayi2;
            yanlisSonuc1 = dogruSonuc + Random.Range(1,4);
            yanlisSonuc2 = dogruSonuc - sayi1;
            yanlisSonuc3 = dogruSonuc + Random.Range(4,6);

    }
    void ToplamaOrta()
    {
            toplama_Panel.SetActive(true);
            cikarma_Panel.SetActive(false);
            carpma_Panel.SetActive(false);
            bolme_Panel.SetActive(false);
            sayi1 = Random.Range(10,51);
            sayi2 = Random.Range(10,51);
            txtSoru_1.text = sayi1.ToString();
            txtSoru_2.text = sayi2.ToString();

            txt_Bolen.text = "";
            txt_Bolunen.text = "";

            dogruSonuc = sayi1 + sayi2;
            yanlisSonuc1 = dogruSonuc + Random.Range(9,15);
            yanlisSonuc2 = dogruSonuc - sayi1;
            yanlisSonuc3 = dogruSonuc + Random.Range(15,20);

    }
    void ToplamaZor()
    {
            toplama_Panel.SetActive(true);
            cikarma_Panel.SetActive(false);
            carpma_Panel.SetActive(false);
            bolme_Panel.SetActive(false);
            sayi1 = Random.Range(101,1000);
            sayi2 = Random.Range(101,1000);
            txtSoru_1.text = sayi1.ToString();
            txtSoru_2.text = sayi2.ToString();
            txt_Bolen.text = "";
            txt_Bolunen.text = "";
            

            dogruSonuc = sayi1 + sayi2;
            yanlisSonuc1 = dogruSonuc + Random.Range(50,70);
            yanlisSonuc2 = dogruSonuc - sayi1;
            yanlisSonuc3 = dogruSonuc + Random.Range(70,80);

    }
    //--------------------------------------------------------------------------------------------------------------------
    void CikartmaKolay()
    {
            sayi1 = Random.Range(1,10);
            sayi2 = Random.Range(1,10);

            if(sayi2 > sayi1)
            {
                txtSoru.text = sayi2.ToString() + " - " + sayi1.ToString() + " = ?";
            }
            else
            {
                txtSoru.text = sayi1.ToString() + " - " + sayi2.ToString() + " = ?";
            }

            dogruSonuc = Mathf.Abs(sayi1 - sayi2);

            do
            {
                yanlisSonuc1 = Random.Range(1,10);
                yanlisSonuc2 = Random.Range(1,10);
                yanlisSonuc3 = Random.Range(1,10);
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));
            
             
    }
    void CikartmaOrta()
    {
            toplama_Panel.SetActive(false);
            cikarma_Panel.SetActive(true);
            carpma_Panel.SetActive(false);
            bolme_Panel.SetActive(false);
            sayi1 = Random.Range(10,100);
            sayi2 = Random.Range(10,100);
            txt_Bolen.text = "";
            txt_Bolunen.text = "";

            if(sayi2 > sayi1)
            {
                txtSoru_1.text = sayi2.ToString();
                txtSoru_2.text = sayi1.ToString();
            }
            else
            {
                txtSoru_1.text = sayi1.ToString();
                txtSoru_2.text = sayi2.ToString();
            }

            dogruSonuc = Mathf.Abs(sayi1 - sayi2);

            do
            {
                yanlisSonuc1 = Random.Range(10,100);
                yanlisSonuc2 = Random.Range(10,40);
                yanlisSonuc3 = Random.Range(40,70);
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));

    }
    void CikartmaZor()
    {
            toplama_Panel.SetActive(false);
            cikarma_Panel.SetActive(true);
            carpma_Panel.SetActive(false);
            bolme_Panel.SetActive(false);
            sayi1 = Random.Range(100,1000);
            sayi2 = Random.Range(100,1000);
            txt_Bolen.text = "";
            txt_Bolunen.text = "";

            if(sayi2 > sayi1)
            {
                txtSoru_1.text = sayi2.ToString();
                txtSoru_2.text = sayi1.ToString();
            }
            else
            {
                txtSoru_1.text = sayi1.ToString();
                txtSoru_2.text = sayi2.ToString();
            }

            dogruSonuc = Mathf.Abs(sayi1 - sayi2);

            do
            {
                yanlisSonuc1 = Random.Range(100,1000);
                yanlisSonuc2 = Random.Range(100,400);
                yanlisSonuc3 = Random.Range(400,700);
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));
        
    }
    //---------------------------------------------------------------------------------------------------------------------
    void CarpmaKolay()
    {
            sayi1 = Random.Range(1,10);
            sayi2 = Random.Range(1,10);

            dogruSonuc = sayi1 * sayi2;

            txtSoru.text = sayi1.ToString() + " x " + sayi2.ToString() + " = ?";
            

            do
            {
                yanlisSonuc1 = sayi1 * Random.Range(1,10);
                yanlisSonuc2 = sayi2 * Random.Range(1,10);
                yanlisSonuc3 = sayi1 * Random.Range(1,10);
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));
             
    }
    void CarpmaOrta()
    {
            txtSoru_2.transform.position = position;
            toplama_Panel.SetActive(false);
            cikarma_Panel.SetActive(false);
            carpma_Panel.SetActive(true);
            bolme_Panel.SetActive(false);
            sayi1 = Random.Range(10,100);
            sayi2 = Random.Range(2,10);
            txt_Bolen.text = "";
            txt_Bolunen.text = "";

            dogruSonuc = sayi1 * sayi2;

            txtSoru_2.transform.position = new Vector3(txtSoru_2.transform.position.x+15, txtSoru_2.transform.position.y, txtSoru_2.transform.position.z);

            txtSoru_1.text = sayi1.ToString();
            txtSoru_2.text = sayi2.ToString();

            do
            {
                yanlisSonuc1 = sayi1 * Random.Range(2,10);
                yanlisSonuc2 = sayi2 * Random.Range(10,100);
                yanlisSonuc3 = sayi1 * Random.Range(2,10);
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));

    }
    void CarpmaZor()
    {
            toplama_Panel.SetActive(false);
            cikarma_Panel.SetActive(false);
            carpma_Panel.SetActive(true);
            bolme_Panel.SetActive(false);
            sayi1 = Random.Range(10,100);
            sayi2 = Random.Range(10,100);
            txt_Bolen.text = "";
            txt_Bolunen.text = "";

            dogruSonuc = sayi1 * sayi2;

            txtSoru_1.text = sayi1.ToString();
            txtSoru_2.text = sayi2.ToString();

            do
            {
                yanlisSonuc1 = sayi1 * Random.Range(10,100);
                yanlisSonuc2 = sayi2 * Random.Range(10,100);
                yanlisSonuc3 = sayi1 * Random.Range(10,100);
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));
        
    }
    //---------------------------------------------------------------------------------------------------------------------
    void BolmeKolay()
    {
            do
            {
                sayi1 = Random.Range(4,10);
                sayi2 = Random.Range(1,5);
                dogruSonuc = sayi1 / sayi2;


            }while(sayi1 % sayi2 != 0);

            txtSoru.text = sayi1.ToString() + " ÷ " + sayi2.ToString() + " = ?";

            do
            {
                yanlisSonuc1 = Random.Range(1,10);
                yanlisSonuc2 = Random.Range(1,10);
                yanlisSonuc3 = Random.Range(1,10);
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));
            
    }
    void BolmeOrta()
    {
            toplama_Panel.SetActive(false);
            cikarma_Panel.SetActive(false);
            carpma_Panel.SetActive(false);
            bolme_Panel.SetActive(true);
            txtSoru_1.text = "";
            txtSoru_2.text = "";
            do
            {
                sayi1 = Random.Range(10,100);
                sayi2 = Random.Range(2,10);
                dogruSonuc = sayi1 / sayi2;


            }while(sayi1 % sayi2 != 0);

            txt_Bolunen.text = sayi1.ToString();
            txt_Bolen.text = sayi2.ToString();

            do
            {
                yanlisSonuc1 = Mathf.Abs(dogruSonuc - (dogruSonuc * Random.Range(1,5)));
                yanlisSonuc2 = dogruSonuc + Random.Range(1,10);
                yanlisSonuc3 = Mathf.Abs(dogruSonuc - Random.Range(1,10));
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));

    }
    void BolmeZor()
    {
            toplama_Panel.SetActive(false);
            cikarma_Panel.SetActive(false);
            carpma_Panel.SetActive(false);
            bolme_Panel.SetActive(true);
            txtSoru_1.text = "";
            txtSoru_2.text = "";
             do
            {
                sayi1 = Random.Range(100,1000);
                sayi2 = Random.Range(10,100);
                dogruSonuc = sayi1 / sayi2;


            }while(sayi1 % sayi2 != 0);

            txt_Bolunen.text = sayi1.ToString();
            txt_Bolen.text = sayi2.ToString();

            do
            {
                yanlisSonuc1 = Mathf.Abs(dogruSonuc - (dogruSonuc * Random.Range(2,20)));
                yanlisSonuc2 = dogruSonuc + Random.Range(10,100);
                yanlisSonuc3 = Mathf.Abs(dogruSonuc - Random.Range(10,100));
            } while ((yanlisSonuc1==yanlisSonuc2)||(yanlisSonuc2==yanlisSonuc3)||(yanlisSonuc1==yanlisSonuc3)||(dogruSonuc==yanlisSonuc1)||(dogruSonuc==yanlisSonuc2)||(dogruSonuc==yanlisSonuc3));
    }
    //---------------------------------------------------------------------------------------------------------------------

    public void SaveData()
    {
            toplamDogruAdet = toplamaD+cikartmaD+carpmaD+bolmeD+karisikD;
            toplamYanlisAdet = toplamaY+cikartmaY+carpmaY+bolmeY+karisikY;

            toplamPuan = toplamaP+cikartmaP+bolmeP+carpmaP+karisikP;
                
            myFile.Add("toplamPuan",toplamPuan);
            myFile.Add("toplamDogru",toplamDogruAdet);
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
    }

    public void LoadData()
    {
        if(myFile.Load()){
        toplamPuan = myFile.GetInt("toplamPuan");
        toplamDogruAdet = myFile.GetInt("toplamDogru");
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

    public void SkorKontrolDogru()
    {
        if(hangiIslem == "toplama" && hangiSeviye == "kolay")
        {
            toplamaD++;
            toplamaP += 10;
        }
          else if (hangiIslem == "toplama" && hangiSeviye == "orta")
        {
            toplamaD++;
            toplamaP += 25;

        }
          else if (hangiIslem == "toplama" && hangiSeviye == "zor")
        {
            toplamaD++;
            toplamaP += 50;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "cikartma" && hangiSeviye == "kolay")
        {
            cikartmaD++;
            cikartmaP += 10;
        }
          else if (hangiIslem == "cikartma" && hangiSeviye == "orta")
        {
            cikartmaD++;
            cikartmaP += 25;
        }
          else if (hangiIslem == "cikartma" && hangiSeviye == "zor")
        {
            cikartmaD++;
            cikartmaP += 50;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "carpma" && hangiSeviye == "kolay")
        {
            carpmaD++;
            carpmaP += 10;
        }
          else if (hangiIslem == "carpma" && hangiSeviye == "orta")
        {
            carpmaD++;
            carpmaP += 25;
        }
          else if (hangiIslem == "carpma" && hangiSeviye == "zor")
        {
            carpmaD++;
            carpmaP += 50;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "bolme" && hangiSeviye == "kolay")
        {
            bolmeD++;
            bolmeP += 10;
        } 
          else if (hangiIslem == "bolme" && hangiSeviye == "orta")
        {
            bolmeD++;
            bolmeP += 25;
        }
          else if (hangiIslem == "bolme" && hangiSeviye == "zor")
        {
            bolmeD++;
            bolmeP += 50;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "karisik" && hangiSeviye == "kolay")
        {
            karisikD++;
            karisikP += 10;
        }
          else if (hangiIslem == "karisik" && hangiSeviye == "orta")
        {
            karisikD++;
            karisikP += 25;
        }
          else if (hangiIslem == "karisik" && hangiSeviye == "zor")
          {
            karisikD++;
            karisikP += 50;
          }
    }

    public void SkorKontrolYanlis()
    {
        if(hangiIslem == "toplama" && hangiSeviye == "kolay")
        {
            toplamaY++;
            toplamaP -= 3;
        }
          else if (hangiIslem == "toplama" && hangiSeviye == "orta")
        {
            toplamaY++;
            toplamaP -= 7;

        }
          else if (hangiIslem == "toplama" && hangiSeviye == "zor")
        {
            toplamaY++;
            toplamaP -= 15;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "cikartma" && hangiSeviye == "kolay")
        {
            cikartmaY++;
            cikartmaP -= 3;

        }
          else if (hangiIslem == "cikartma" && hangiSeviye == "orta")
        {
            cikartmaY++;
            cikartmaP -= 7;

        }
          else if (hangiIslem == "cikartma" && hangiSeviye == "zor")
        {
            cikartmaY++;
            cikartmaP -= 15;

        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "carpma" && hangiSeviye == "kolay")
        {
            carpmaY++;
            carpmaP -= 3;

        }
          else if (hangiIslem == "carpma" && hangiSeviye == "orta")
        {
            carpmaY++;
            carpmaP -= 7;

        }
          else if (hangiIslem == "carpma" && hangiSeviye == "zor")
        {
            carpmaY++;
            carpmaP -= 15;

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "bolme" && hangiSeviye == "kolay")
        {
            bolmeY++;
            bolmeP -= 3;

        } 
          else if (hangiIslem == "bolme" && hangiSeviye == "orta")
        {
            bolmeY++;
            bolmeP -= 7;

        }
          else if (hangiIslem == "bolme" && hangiSeviye == "zor")
        {
            bolmeY++;
            bolmeP -= 15;

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
          else if (hangiIslem == "karisik" && hangiSeviye == "kolay")
        {
            karisikY++;
            karisikP -= 3;

        }
          else if (hangiIslem == "karisik" && hangiSeviye == "orta")
        {
            karisikY++;
            karisikP -= 7;

        }
          else if (hangiIslem == "karisik" && hangiSeviye == "zor")
          {
            karisikY++;
            karisikP -= 15;
          }
    }




   public void BtnAAktif()
    {
        btnA.GetComponent<Image>().sprite = imgAktif_A;
   }

   public void BtnAPasif()
   {

        if(butonClickCheck=="a")
        {
            btnA.GetComponent<Image>().sprite = imgDogruBtn;
            Btn_Panel_Ac();
                if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioCorrect;  
                   audioSource.Play();
               }

            dogruAdet++;

            if (hangiSeviye == "kolay")
            {
                puan += 10;
                
            }
            else if (hangiSeviye == "orta")
            {
                puan += 25;
                
            }
            else if (hangiSeviye == "zor")
            {
                puan += 50;
                
            }

            txt_Puan.text = puan.ToString();

            SkorKontrolDogru();
            StartCoroutine(SoruYazCourite());

        }
        else
        {

            yanlisAdet++;
            if (hangiSeviye == "kolay")
            {
                puan -= 3;
                
            }
            else if (hangiSeviye == "orta")
            {
                puan -= 7;
                
            }
            else if (hangiSeviye == "zor")
            {
                puan -= 15;
                
            }
            txt_Puan.text = puan.ToString();
            SkorKontrolYanlis();

                if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioFalse;  
                   audioSource.Play();
               }

            btnA.GetComponent<Image>().sprite = imgPasif_D;
            btnA_Panel.SetActive(true);
        }
       
       
   }

   IEnumerator SoruYazCourite()
   {
    yield return new WaitForSeconds(1f);
    SoruYaz();

   }

   public void BtnBAktif()
   {
       btnB.GetComponent<Image>().sprite = imgAktif_A;

   }
   public void BtnBPasif()
   {
           if(butonClickCheck=="b")
        {
            btnB.GetComponent<Image>().sprite = imgDogruBtn;
                if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioCorrect;  
                   audioSource.Play();
               }

            Btn_Panel_Ac();
            dogruAdet++;

            if (hangiSeviye == "kolay")
            {
                puan += 10;

            }
            else if (hangiSeviye == "orta")
            {
                puan += 25;

            }
            else if (hangiSeviye == "zor")
            {
                puan += 50;

            }
            txt_Puan.text = puan.ToString();

            SkorKontrolDogru();
            StartCoroutine(SoruYazCourite());

        }
        else
        {
            btnB.GetComponent<Image>().sprite = imgPasif_D;
            if (hangiSeviye == "kolay")
            {
                puan -= 3;
                
            }
            else if (hangiSeviye == "orta")
            {
                puan -= 7;
                
            }
            else if (hangiSeviye == "zor")
            {
                puan -= 15;
                
            }
            txt_Puan.text = puan.ToString();
            yanlisAdet++;
                 if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioFalse;  
                   audioSource.Play();
               }

            SkorKontrolYanlis();
            btnB_Panel.SetActive(true);
        }

   }

   public void BtnCAktif()
   {
       btnC.GetComponent<Image>().sprite = imgAktif_A;

   }
   public void BtnCPasif()
   {
            if(butonClickCheck=="c")
        {
            btnC.GetComponent<Image>().sprite = imgDogruBtn;
              if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioCorrect;  
                   audioSource.Play();
               }

            Btn_Panel_Ac();
            dogruAdet++;


            if (hangiSeviye == "kolay")
            {
                puan += 10;

            }
            else if (hangiSeviye == "orta")
            {
                puan += 25;

            }
            else if (hangiSeviye == "zor")
            {
                puan += 50;

            }
            txt_Puan.text = puan.ToString();

            SkorKontrolDogru();

            StartCoroutine(SoruYazCourite());

        }
        else
        {
            btnC.GetComponent<Image>().sprite = imgPasif_D;
            if (hangiSeviye == "kolay")
            {
                puan -= 3;
                
            }
            else if (hangiSeviye == "orta")
            {
                puan -= 7;
                
            }
            else if (hangiSeviye == "zor")
            {
                puan -= 15;
                
            }
            txt_Puan.text = puan.ToString();
            yanlisAdet++;
            if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioFalse;  
                   audioSource.Play();
               }

            SkorKontrolYanlis();
            btnC_Panel.SetActive(true);
        }

   }

   public void BtnDAktif()
   {
       btnD.GetComponent<Image>().sprite = imgAktif_A;
   }
   public void BtnDPasif()
   {
            if(butonClickCheck=="d")
        {
            btnD.GetComponent<Image>().sprite = imgDogruBtn;
              if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioCorrect;  
                   audioSource.Play();
               }

            Btn_Panel_Ac();
            dogruAdet++;

            if (hangiSeviye == "kolay")
            {
                puan += 10;

            }
            else if (hangiSeviye == "orta")
            {
                puan += 25;

            }
            else if (hangiSeviye == "zor")
            {
                puan += 50;

            }
            txt_Puan.text = puan.ToString();
            SkorKontrolDogru();
            StartCoroutine(SoruYazCourite());

        }
        else
        {
            btnD.GetComponent<Image>().sprite = imgPasif_D;
            if (hangiSeviye == "kolay")
            {
                puan -= 3;
                
            }
            else if (hangiSeviye == "orta")
            {
                puan -= 7;
                
            }
            else if (hangiSeviye == "zor")
            {
                puan -= 15;
                
            }
            txt_Puan.text = puan.ToString();
               if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
               else
               {
                   audioSource.clip = audioFalse;  
                   audioSource.Play();
               }

            yanlisAdet++;
            SkorKontrolYanlis();
            btnD_Panel.SetActive(true);

        }

   }


   public void BtnCikis()
    {   
        cikisPanel.SetActive(true);
        kapatmaPanel.SetActive(true);
        
    }

   public void BtnCikisPanelKapat()
    {
        cikisPanel.SetActive(false);
        kapatmaPanel.SetActive(false);
    }

   public IEnumerator LoadScene_Couritine()
   {
    yield return new WaitForSeconds(1f);
    Destroy(gameObject);
    SceneManager.LoadScene(1);

   }


   public void BtnCikisEvet()
    {    
        
        adController.bannerView.Hide();
        SaveData();
       
        transition_Circle.transform.DOLocalMoveY(3380f,0.8f).SetEase(Ease.OutQuint);

        cetvel.transform.DOShakeRotation(1,new Vector3(0,0,25));
        kalem.transform.DOShakeRotation(1,new Vector3(0,0,-20));

                if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
                else
               {
                   audioSource.clip = audioTransition;  
                   audioSource.Play();
               }

        

        StartCoroutine(LoadScene_Couritine());
        
    }

   public void Btn_Panel_Ac()
   {
        btnA_Panel.SetActive(true);
        btnB_Panel.SetActive(true);
        btnC_Panel.SetActive(true);
        btnD_Panel.SetActive(true);
    
   }

   void Rozet_Move (int kacincirozet)
   {
    
        rozetler_img.sprite = rozetler[kacincirozet];

        if (kacincirozet == 0 || kacincirozet == 5 || kacincirozet == 10 || kacincirozet == 15 || kacincirozet == 20)
        {
            txtRozet.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","rozet1");
        }
            
        else if (kacincirozet == 1 || kacincirozet == 6 || kacincirozet == 11 || kacincirozet == 16 || kacincirozet == 21)
        {
            txtRozet.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","rozet10");

        }
        else if (kacincirozet == 2 || kacincirozet == 7 || kacincirozet == 12 || kacincirozet == 17 || kacincirozet == 22)
        {
            txtRozet.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","rozet100");

        }
        else if (kacincirozet == 3 || kacincirozet == 8 || kacincirozet == 13 || kacincirozet == 18 || kacincirozet == 23)
        {
            txtRozet.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","rozet500");

        }
        else if (kacincirozet == 4 || kacincirozet == 9 || kacincirozet == 14 || kacincirozet == 19 || kacincirozet == 24)
        {
            txtRozet.text = LocalizationSettings.StringDatabase.GetLocalizedString("txtLangTables","rozet1000");

        }

        if (acKapaSes == false)
               {
                    audioSource.Stop();
               }
        else
               {
                   audioSource.clip = audioRozet;  
                   audioSource.Play();
               }

        kararmaUstPanel.SetActive(true);

        rozet_basari.transform.DOLocalMoveY(1350, 1).SetEase(Ease.OutBack).OnComplete(() => 
        {
            // 1 saniye bekle
            DOVirtual.DelayedCall(1, () => 
            {
                // GameObject'i başlangıç pozisyonuna döndür
                rozet_basari.transform.DOLocalMoveY(1700, 1);
                kararmaUstPanel.SetActive(false);
            });
        });

   }
}
    







