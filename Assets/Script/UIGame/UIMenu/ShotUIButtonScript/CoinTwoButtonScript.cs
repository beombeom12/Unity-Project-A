using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTwoButtonScript : MonoBehaviour
{
    public   bool bPointDown = false;
    public AudioClip clip;
    public GameObject CoinAnim;
    public void DecreaseDiamond(float fDiamond)
    {
        MenuData.Instance.fDiamonCurrent -= fDiamond;

        if(MenuData.Instance.fDiamonCurrent <= 0)
        {
            MenuData.Instance.fDiamonCurrent = 0;
        }

    }


    public void IncreaseShooter(float fShooter)
    {
        MenuData.Instance.fCurrentShooter += fShooter;
        if(MenuData.Instance.fCurrentShooter >= MenuData.Instance.fShooterMax)
        {
            MenuData.Instance.fCurrentShooter = MenuData.Instance.fShooterMax;
        }
    
    
    }

    public void ClickDown()
    {
        bPointDown = true;
        Instantiate(CoinAnim, transform.position, transform.rotation);
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        
        DecreaseDiamond(80);

        StartCoroutine(DelayClick());
    }


    IEnumerator DelayClick()
    {
        yield return null;

        yield return new WaitForSeconds(0.83f);

        IncreaseShooter(4320f);
    }

    public void ClickUp()
    {
        bPointDown = false;

    }

}
