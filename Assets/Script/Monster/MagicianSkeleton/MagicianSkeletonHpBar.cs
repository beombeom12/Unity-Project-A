using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicianSkeletonHpBar : MonoBehaviour
{

    public static MagicianSkeletonHpBar Instance
    {
        get
        {
            if (MagicianHpInstance == null)
            {
                MagicianHpInstance = FindObjectOfType<MagicianSkeletonHpBar>();
                if (MagicianHpInstance == null)
                {
                    var InstanceContainer = new GameObject("MagicianSkeletonHpBar");
                    MagicianHpInstance = InstanceContainer.AddComponent<MagicianSkeletonHpBar>();
                }
            }
            return MagicianHpInstance;
        }
    }

    private static MagicianSkeletonHpBar MagicianHpInstance;

    public Slider MagicianSkHpBar;
    public Slider LateHpBar;

    public Transform magicianskeleton;

    public float fMagicianSkHp = 10000f;
    public float fMagicianSkMaxHp = 10000f;

    public bool bLateHit = false;



    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(magicianskeleton.transform.position.x, magicianskeleton.transform.position.y + 3f, magicianskeleton.transform.position.z);

        MagicianSkHpBar.value = Mathf.Lerp(MagicianSkHpBar.value, fMagicianSkHp / fMagicianSkMaxHp, Time.deltaTime * 5f);
        if(bLateHit)
        {
            LateHpBar.value = Mathf.Lerp(LateHpBar.value, MagicianSkHpBar.value, Time.deltaTime * 1f);
        }
    }



    public void Damage(float fDamage)
    {
        fMagicianSkHp -= fDamage;


        Invoke("LateDamage", 0.7f);
    }


    public void LateDamage()
    {
        bLateHit = true;
    }
}
