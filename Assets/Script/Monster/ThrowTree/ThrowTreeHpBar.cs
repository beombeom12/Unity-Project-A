using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThrowTreeHpBar : MonoBehaviour
{
    public static ThrowTreeHpBar Instance
    {
        get
        {
            if (ThrowTreeHPInstance == null)
            {
                ThrowTreeHPInstance = FindObjectOfType<ThrowTreeHpBar>();
                if (ThrowTreeHPInstance == null)
                {
                    var InstanceContainer = new GameObject("ThrowTreeHpBar");
                    ThrowTreeHPInstance = InstanceContainer.AddComponent<ThrowTreeHpBar>();
                }
            }
            return ThrowTreeHPInstance;
        }
    }
    private static ThrowTreeHpBar ThrowTreeHPInstance;



    public Slider ThrowTreeHPSlider;
    public Slider LateHpSlider;

    public Transform ThrowTree;

    public float fThrowTreeHp = 1290;
    public float fThrowTreeMaxHp = 1290f;

    public bool bLateBackHit = false;

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(ThrowTree.transform.position.x, ThrowTree.transform.position.y + 1f, ThrowTree.transform.position.z);

        ThrowTreeHPSlider.value = Mathf.Lerp(ThrowTreeHPSlider.value, fThrowTreeHp / fThrowTreeMaxHp, Time.deltaTime * 5f);
        
        if (bLateBackHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, ThrowTreeHPSlider.value, Time.deltaTime * 1f);
        }

    }


    public void Damage(float fDamage)
    {
        fThrowTreeHp -= fDamage;
        Invoke("LateDamage", 0.7f);
    }


    public void LateDamage()
    {
        bLateBackHit = true;
    }

}
