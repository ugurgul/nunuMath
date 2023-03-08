using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRewarded : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetBindings();
    }

    private void SetBindings()
    {
        //Rewarded reklam tıklandığında GiveGold Fonksiyonunu çağır
        var rewardedButton = GetComponent<Button>();
        rewardedButton.onClick.AddListener(()=>
        {
            AdManager.Instance.ShowRewarded(GiveGold);

        });
    }

    public void GiveGold(bool result)
    {
        if(result)
        {
            Debug.Log("Altın kazanıldı");
        }
        else
        {
            Debug.Log("Ödül başarısız");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
