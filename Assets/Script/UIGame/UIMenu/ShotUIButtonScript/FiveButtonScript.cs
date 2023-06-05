using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveButtonScript : MonoBehaviour
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

        StartCoroutine(Delayclick());
    
    }

    IEnumerator Delayclick()
    {
        yield return null;

        yield return new WaitForSeconds(0.83f);

        IncreaseDiamond(6500f);

    }


    public void ClickUp()
    {
        PointDown = false;
    }
}
