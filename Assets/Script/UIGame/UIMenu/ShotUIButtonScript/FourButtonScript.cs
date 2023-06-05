using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourButtonScript : MonoBehaviour
{
    public bool PointDown;
    public AudioClip clip;
    public GameObject DiaAnim;
    public void IncreaseDiamond(float fDiamond)
    {
        MenuData.Instance.fDiamonCurrent += fDiamond;
        if (MenuData.Instance.fDiamonCurrent >= MenuData.Instance.fDiamonMax)
        {
            MenuData.Instance.fDiamonCurrent = MenuData.Instance.fDiamonMax;
        }

    }

    public void ClickDown()
    {
        Instantiate(DiaAnim, transform.position, transform.rotation);
        PointDown = true;
        SoundManager.Instance.ButtonSound("ButtonClick", clip);

        StartCoroutine(DelayClick());
    
    }


    IEnumerator DelayClick()
    {
        yield return null;

        yield return new WaitForSeconds(0.83f);

        IncreaseDiamond(2500f);
    }


    public void ClickUp()
    {
        PointDown = false;
    }
}
