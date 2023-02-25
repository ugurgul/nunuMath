using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MainAnim : MonoBehaviour
{
    public int speed = 30;

    [SerializeField]
     GameObject sun,cloud1,cloud2;

    bool flipFlop = false;


    void Update()
    {
         sun.transform.Rotate(Vector3.forward*speed*Time.deltaTime);

    }

    private void Start() {
        StartCoroutine(CloudMovement());

        sun.transform.DOShakeScale(1,3);


    }
          IEnumerator CloudMovement()
        {
            while (true)
            {
                if(flipFlop == false)
            {

            
            cloud2.transform.DOMoveX(170,120);
            cloud1.transform.DOMoveX(170,60);
            yield return new WaitForSeconds(25f);
            flipFlop = true;
            }
            else if(flipFlop == true)
            {
            cloud2.transform.DOMoveX(80,120);
            cloud1.transform.DOMoveX(80,60);
            yield return new WaitForSeconds(25f);
            flipFlop = false;
            }
            }


        }
}
