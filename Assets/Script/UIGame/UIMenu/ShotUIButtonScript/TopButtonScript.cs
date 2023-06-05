using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopButtonScript : MonoBehaviour
{


     public bool PointDown;
    public AudioClip clip;
    public GameObject[] TopDiaButton;
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

        for(int i = 0; i < 7; i++)
        { 
        Instantiate(TopDiaButton[i], transform.position, transform.rotation);

        }



        PointDown = true;
        SoundManager.Instance.ButtonSound("ButtonClick", clip);


        StartCoroutine(DelayClick());
  
    }


    IEnumerator DelayClick()
    {
        yield return null;

        yield return new WaitForSeconds(0.83f);
        IncreaseDiamond(800f);
    }


    public void ClickUp()
    {
        PointDown = false;
    }

}
