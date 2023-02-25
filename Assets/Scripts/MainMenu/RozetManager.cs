using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RozetManager : MonoBehaviour
{

    MainMenuManager mainMenuManager;


[SerializeField]
private GameObject parilti,rozetPanel,kapatmaPanel;

[SerializeField]
private AudioSource audioSource;

[SerializeField]
private AudioClip audioClick,audioClosed;

[SerializeField]
public Image toplama1,toplama10,toplama100,toplama500,toplama1000;
[SerializeField]
public Image cikarma1,cikarma10,cikarma100,cikarma500,cikarma1000;
[SerializeField]
public Image carpma1,carpma10,carpma100,carpma500,carpma1000;
[SerializeField]
public Image bolme1,bolme10,bolme100,bolme500,bolme1000;
[SerializeField]
public Image karisik1,karisik10,karisik100,karisik500,karisik1000;

public int speed = 30;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainMenuManager = Object.FindObjectOfType<MainMenuManager>();

    }

    // Update is called once per frame
    void Update()
    {
        parilti.transform.Rotate(Vector3.forward*speed*Time.deltaTime);
    }

    public void BtnRozetPanelKapat()
    {
        audioSource.PlayOneShot(audioClosed);
        kapatmaPanel.SetActive(false);
        rozetPanel.GetComponent<CanvasGroup>().DOFade(0,0.5f);
        rozetPanel.GetComponent<RectTransform>().DOScale(0,0.5f).SetEase(Ease.OutBack); 

    }

    public void BtnRozetPanelAc()
    {
        audioSource.PlayOneShot(audioClick);
        rozetPanel.GetComponent<CanvasGroup>().DOFade(1,0.5f);
        rozetPanel.GetComponent<RectTransform>().DOScale(1,0.5f).SetEase(Ease.OutBack);
        kapatmaPanel.SetActive(true);


        
        Debug.Log(mainMenuManager.toplamaD);

        if(mainMenuManager.toplamaD >= 1 && mainMenuManager.toplamaD <= 9)
        {
        MakeImageTransparent(toplama1,1f);
        }
        else if (mainMenuManager.toplamaD >= 10 && mainMenuManager.toplamaD <= 99)
        {
        MakeImageTransparent(toplama1,1f);
        MakeImageTransparent(toplama10,1f);

        }
        else if (mainMenuManager.toplamaD >= 100  && mainMenuManager.toplamaD <= 499)
        {
        MakeImageTransparent(toplama1,1f);
        MakeImageTransparent(toplama10,1f);
        MakeImageTransparent(toplama100,1f);
        }
        else if (mainMenuManager.toplamaD >= 500 && mainMenuManager.toplamaD <= 999)
        {
        MakeImageTransparent(toplama1,1f);
        MakeImageTransparent(toplama10,1f);
        MakeImageTransparent(toplama100,1f);
        MakeImageTransparent(toplama500,1f);
        }
        else if (mainMenuManager.toplamaD > 999)
        {
        MakeImageTransparent(toplama1,1f);
        MakeImageTransparent(toplama10,1f);
        MakeImageTransparent(toplama100,1f);
        MakeImageTransparent(toplama500,1f);
        MakeImageTransparent(toplama1000,1f);
        }


        if(mainMenuManager.cikartmaD >= 1 && mainMenuManager.cikartmaD <= 9)
        {
            
        MakeImageTransparent(cikarma1,1f);
        }
        else if (mainMenuManager.cikartmaD >= 10 && mainMenuManager.cikartmaD <= 99)
        {
        MakeImageTransparent(cikarma1,1f);
        MakeImageTransparent(cikarma10,1f);

        }
        else if (mainMenuManager.cikartmaD >= 100  && mainMenuManager.cikartmaD <= 499)
        {
        MakeImageTransparent(cikarma1,1f);
        MakeImageTransparent(cikarma10,1f);
        MakeImageTransparent(cikarma100,1f);
        }
        else if (mainMenuManager.cikartmaD >= 500 && mainMenuManager.cikartmaD <= 999)
        {
        MakeImageTransparent(cikarma1,1f);
        MakeImageTransparent(cikarma10,1f);
        MakeImageTransparent(cikarma100,1f);
        MakeImageTransparent(cikarma500,1f);
        }
        else if (mainMenuManager.cikartmaD > 999)
        {
        MakeImageTransparent(cikarma1,1f);
        MakeImageTransparent(cikarma10,1f);
        MakeImageTransparent(cikarma100,1f);
        MakeImageTransparent(cikarma500,1f);
        MakeImageTransparent(cikarma1000,1f);
        }


        if(mainMenuManager.carpmaD >= 1 && mainMenuManager.carpmaD <= 9)
        {
        MakeImageTransparent(carpma1,1f);
        }
        else if (mainMenuManager.carpmaD >= 10 && mainMenuManager.carpmaD <= 99)
        {
        MakeImageTransparent(carpma1,1f);
        MakeImageTransparent(carpma10,1f);

        }
        else if (mainMenuManager.carpmaD >= 100  && mainMenuManager.carpmaD <= 499)
        {
        MakeImageTransparent(carpma1,1f);
        MakeImageTransparent(carpma10,1f);
        MakeImageTransparent(carpma100,1f);
        }
        else if (mainMenuManager.carpmaD >= 500 && mainMenuManager.carpmaD <= 999)
        {
        MakeImageTransparent(carpma1,1f);
        MakeImageTransparent(carpma10,1f);
        MakeImageTransparent(carpma100,1f);
        MakeImageTransparent(carpma500,1f);
        }
        else if (mainMenuManager.carpmaD > 999)
        {
        MakeImageTransparent(carpma1,1f);
        MakeImageTransparent(carpma10,1f);
        MakeImageTransparent(carpma100,1f);
        MakeImageTransparent(carpma500,1f);
        MakeImageTransparent(carpma1000,1f);
        }


        if(mainMenuManager.bolmeD >= 1 && mainMenuManager.bolmeD <= 9)
        {
        MakeImageTransparent(bolme1,1f);
        }
        else if (mainMenuManager.bolmeD >= 10 && mainMenuManager.bolmeD <= 99)
        {
        MakeImageTransparent(bolme1,1f);
        MakeImageTransparent(bolme10,1f);

        }
        else if (mainMenuManager.bolmeD >= 100  && mainMenuManager.bolmeD <= 499)
        {
        MakeImageTransparent(bolme1,1f);
        MakeImageTransparent(bolme10,1f);
        MakeImageTransparent(bolme100,1f);
        }
        else if (mainMenuManager.bolmeD >= 500 && mainMenuManager.bolmeD <= 999)
        {
        MakeImageTransparent(bolme1,1f);
        MakeImageTransparent(bolme10,1f);
        MakeImageTransparent(bolme100,1f);
        MakeImageTransparent(bolme500,1f);
        }
        else if (mainMenuManager.bolmeD > 999)
        {
        MakeImageTransparent(bolme1,1f);
        MakeImageTransparent(bolme10,1f);
        MakeImageTransparent(bolme100,1f);
        MakeImageTransparent(bolme500,1f);
        MakeImageTransparent(bolme1000,1f);
        }


        if(mainMenuManager.karisikD >= 1 && mainMenuManager.karisikD <= 9)
        {
        MakeImageTransparent(karisik1,1f);
        }
        else if (mainMenuManager.karisikD >= 10 && mainMenuManager.karisikD <= 99)
        {
        MakeImageTransparent(karisik1,1f);
        MakeImageTransparent(karisik10,1f);

        }
        else if (mainMenuManager.karisikD >= 100  && mainMenuManager.karisikD <= 499)
        {
        MakeImageTransparent(karisik1,1f);
        MakeImageTransparent(karisik10,1f);
        MakeImageTransparent(karisik100,1f);
        }
        else if (mainMenuManager.karisikD >= 500 && mainMenuManager.karisikD <= 999)
        {
        MakeImageTransparent(karisik1,1f);
        MakeImageTransparent(karisik10,1f);
        MakeImageTransparent(karisik100,1f);
        MakeImageTransparent(karisik500,1f);
        }
        else if (mainMenuManager.karisikD > 999)
        {
        MakeImageTransparent(karisik1,1f);
        MakeImageTransparent(karisik10,1f);
        MakeImageTransparent(karisik100,1f);
        MakeImageTransparent(karisik500,1f);
        MakeImageTransparent(karisik1000,1f);
        }


    }

public void MakeImageTransparent(Image myImage,float transparent) 
{
    Color tempColor = myImage.color;
    tempColor.a = transparent;
    myImage.color = tempColor;
}







}
