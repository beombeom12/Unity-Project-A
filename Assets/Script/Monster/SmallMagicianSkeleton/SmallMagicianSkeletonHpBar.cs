using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmallMagicianSkeletonHpBar : MonoBehaviour
{
    public static SmallMagicianSkeletonHpBar Instance
    {
        get
        {
            if (SmallMagicianHpBarInstance == null)
            {
                SmallMagicianHpBarInstance = FindObjectOfType<SmallMagicianSkeletonHpBar>();
                if (SmallMagicianHpBarInstance == null)
                {
                    var InstanceContainer = new GameObject("SmallMagicianSkeletonHpBar");
                    SmallMagicianHpBarInstance = InstanceContainer.AddComponent<SmallMagicianSkeletonHpBar>();
                }
            }
            return SmallMagicianHpBarInstance;
        }
    }

    private static SmallMagicianSkeletonHpBar SmallMagicianHpBarInstance;




    public Slider SmallmagicianHpSlider;
    public Slider LateHpSlider;

    public Transform SmallMagician;

    public float m_fMaxHp = 1450f;
    public float m_fCurrentHp = 1450f;

    public bool bLateBackHit = false;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(SmallMagician.transform.position.x, SmallMagician.transform.position.y + 1f, SmallMagician.transform.position.z);

        SmallmagicianHpSlider.value = Mathf.Lerp(SmallmagicianHpSlider.value, m_fCurrentHp / m_fMaxHp, Time.deltaTime * 5f);


        if (bLateBackHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, SmallmagicianHpSlider.value, Time.deltaTime * 1f);
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
