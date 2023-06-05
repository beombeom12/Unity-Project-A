using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtonScript : MonoBehaviour
{
   public bool PointDown;
    public GameObject DiaAnim;
    public AudioClip clip;
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

        IncreaseDiamond(500f);
    }


    public void ClickUp()
    {
        PointDown = false;
    }
}
