using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SplashManagment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeScreen",3f);
    }

    void ChangeScreen()
    {
        SceneManager.LoadScene(1);
    }


}
