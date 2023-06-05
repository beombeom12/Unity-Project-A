using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThreeButtonScript : MonoBehaviour
{

    public bool bPointDown;
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

        DecreaseDiamond(200f);
        StartCoroutine(DelayClick());

    }


    IEnumerator DelayClick()
    {
        yield return null;

        yield return new WaitForSeconds(0.83f);

        IncreaseShooter(12960f);
    }

    public void ClickUp()
    {
        bPointDown = false;
    }


}
