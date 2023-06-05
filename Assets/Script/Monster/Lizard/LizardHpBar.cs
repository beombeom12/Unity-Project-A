using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LizardHpBar : MonoBehaviour
{
    public static LizardHpBar Instance
    {
        get
        {
            if (LizardHpInstance == null)
            {
                LizardHpInstance = FindObjectOfType<LizardHpBar>();
                if (LizardHpInstance == null)
                {
                    var InstanceContainer = new GameObject("LizardHpBar");
                    LizardHpInstance = InstanceContainer.AddComponent<LizardHpBar>();
                }
            }
            return LizardHpInstance;
        }
    }

    private static LizardHpBar LizardHpInstance;

    public Slider LizardHpSlider;
    public Slider LizardLateSlider;

    public Transform Lizard;

    public float fLizardHp = 1500f;
    public float fLizardMaxhp = 1500f;

    public bool bLatebackHit = false;

    // Start is called before the first frame update
    void Update()
    {

        transform.position = new Vector3(Lizard.transform.position.x, Lizard.transform.position.y + 1f, Lizard.transform.position.z);

        LizardHpSlider.value = Mathf.Lerp(LizardHpSlider.value, fLizardHp / fLizardMaxhp, Time.deltaTime * 5f);

        if(bLatebackHit)
        {

        LizardLateSlider.value = Mathf.Lerp(LizardLateSlider.value, LizardHpSlider.value, Time.deltaTime * 1f);
        }


    }

   


    public void Damage(float fDamage)
    {
        fLizardHp -= fDamage;

        Invoke("LateDamage", 0.7f);

    }

    public void LateDamage()
    {
        bLatebackHit = true;
    }


}
