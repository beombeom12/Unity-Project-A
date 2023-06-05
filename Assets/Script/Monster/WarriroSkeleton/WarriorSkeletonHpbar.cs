using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorSkeletonHpbar : MonoBehaviour
{

    public Slider WarriorSkeletonHpSlider;
    public Slider LateHpSlider;

    public Transform warriorskeleton;

    public bool bLateHit = false;

    public float fWAHp = 20000f;
    public float fWAMaxHp = 20000f;

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(warriorskeleton.transform.position.x, warriorskeleton.transform.position.y + 1f, warriorskeleton.transform.position.z);

        WarriorSkeletonHpSlider.value = Mathf.Lerp(WarriorSkeletonHpSlider.value, fWAHp / fWAMaxHp, Time.deltaTime * 5f);

        if(bLateHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, WarriorSkeletonHpSlider.value, Time.deltaTime * 1f);
        }



    }

    public void Damage(float fDamage)
    {
        fWAHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }


    public void LateDamage()
    {
        bLateHit = true;
    }
}
