using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpShotHpBar : MonoBehaviour
{

    public static UpShotHpBar Instance
    {
        get
        {
            if (UpShotHpBarInstance == null)
            {
                UpShotHpBarInstance = FindObjectOfType<UpShotHpBar>();
                if (UpShotHpBarInstance == null)
                {
                    var InstanceContainer = new GameObject("UpShotHpBar");
                    UpShotHpBarInstance = InstanceContainer.AddComponent<UpShotHpBar>();
                }
            }
            return UpShotHpBarInstance;
        }
    }
    private static UpShotHpBar UpShotHpBarInstance;

    public Slider UpShotHpSlider;
    public Slider UpShotLateSlider;

    public Transform UpShotMosnter;

    public float fUpShotMonsterHp = 1450f;
    public float fUpShotMaxHp = 1450f;

    public bool bLateBackHit = false;




    // Start is called before the first frame update
    void Update()
    {

        transform.position = new Vector3(UpShotMosnter.transform.position.x, UpShotMosnter.transform.position.y + 1f, UpShotMosnter.transform.position.z);

        UpShotHpSlider.value = Mathf.Lerp(UpShotHpSlider.value, fUpShotMonsterHp / fUpShotMaxHp, Time.deltaTime * 5f);

        if(bLateBackHit)
        {
            UpShotLateSlider.value = Mathf.Lerp(UpShotLateSlider.value, UpShotHpSlider.value, Time.deltaTime * 1f);
        }


    }


    public void Damage(float fDamage)
    {
        fUpShotMonsterHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDamage()
    {
        bLateBackHit = true;
    }


}
