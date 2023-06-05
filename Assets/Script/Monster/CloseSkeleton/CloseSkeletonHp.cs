using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseSkeletonHp : MonoBehaviour
{


    public Slider CloseSkeletonHpbar;
    public Slider LateHpBar;

    public Transform CloseSkeletonChase;

    public float fCloseSkeletonHp = 2300f;
    public float fCloseSkeletonMaxHp = 2300f;

    public bool bLatehHit;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(CloseSkeletonChase.transform.position.x, CloseSkeletonChase.transform.position.y + 1f, CloseSkeletonChase.transform.position.z);
        CloseSkeletonHpbar.value = Mathf.Lerp(CloseSkeletonHpbar.value, fCloseSkeletonHp / fCloseSkeletonMaxHp, Time.deltaTime * 5f);
        
        if(bLatehHit)
        {
            LateHpBar.value = Mathf.Lerp(LateHpBar.value, CloseSkeletonHpbar.value, Time.deltaTime * 1f);
        }
    }

    public void Damage(float fDamage)
    {
        fCloseSkeletonHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDaamage()
    {
        bLatehHit = true;
    }
}
