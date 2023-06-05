using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmallWarriorSkeletonHpBar : MonoBehaviour
{
    public Slider SmallWarriorSkeletonHpSlider;
    public Slider LateHpSlider;
    public Transform SnakkWarrior;
    public float m_fMaxHp = 8000;
    public float m_fCurrentHp = 8000f;


    public bool bLateBackHit = false;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(SnakkWarrior.transform.position.x, SnakkWarrior.transform.position.y + 1f, SnakkWarrior.transform.position.z);

        SmallWarriorSkeletonHpSlider.value = Mathf.Lerp(SmallWarriorSkeletonHpSlider.value, m_fCurrentHp / m_fMaxHp, Time.deltaTime * 5f);


        if (bLateBackHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, SmallWarriorSkeletonHpSlider.value, Time.deltaTime * 1f);
        }
    }


    public void Damage(float fDamage)
    {
        m_fCurrentHp -= fDamage;


        Invoke("LateDamage", 0.7f);
    }


    public void LateDamage()
    {
        bLateBackHit = true;
    }

}
