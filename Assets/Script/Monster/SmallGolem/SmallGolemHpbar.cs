using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallGolemHpbar : MonoBehaviour
{
    public Slider SmallGolemHpSlider;
    public Slider LateGolemSlider;

    public Transform SmallGolem;

    public float fSmallGolemHp = 1690f;
    public float fSmallGolemMaxHp = 1690f;


    public bool bLateHit = false;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(SmallGolem.transform.position.x, SmallGolem.transform.position.y + 1f, SmallGolem.transform.position.z);
        SmallGolemHpSlider.value = Mathf.Lerp(SmallGolemHpSlider.value, fSmallGolemHp / fSmallGolemMaxHp, Time.deltaTime * 5f);

        if(bLateHit)
        {
            LateGolemSlider.value = Mathf.Lerp(LateGolemSlider.value, SmallGolemHpSlider.value, Time.deltaTime * 1f);
        }


    }


    public void Damage(float fDamage)
    {
        fSmallGolemHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }


    public void LateDamage()
    {
        bLateHit = true;
    }
}
