using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Shooter3BoundHp : MonoBehaviour
{
    public static Shooter3BoundHp Instance
    {
        get
        {
            if (Shooter3boundHpInstance == null)
            {
                Shooter3boundHpInstance = FindObjectOfType<Shooter3BoundHp>();
                if (Shooter3boundHpInstance == null)
                {
                    var InstanceContainer = new GameObject("Shooter3BoundHp");
                    Shooter3boundHpInstance = InstanceContainer.AddComponent<Shooter3BoundHp>();
                }
            }
            return Shooter3boundHpInstance;
        }
    }
    private static Shooter3BoundHp Shooter3boundHpInstance; 


    public Slider Shooter3BoundHpSlider;
    public Slider Shooter3boundLateHpSlider;

    public Transform Shooter3Bound;


    public float fShooter3BoundHp = 1500f;
    public float fShooter3BoundHpMax = 1500f;


    public bool bLateBgHit = false;

    // Update is called once per frame
    void Update()
    {
        Shooter3BoundHpSlider.value = Mathf.Lerp(Shooter3BoundHpSlider.value, fShooter3BoundHp / fShooter3BoundHpMax, Time.deltaTime * 5f);

        if(bLateBgHit)
        {
            Shooter3boundLateHpSlider.value = Mathf.Lerp(Shooter3boundLateHpSlider.value, Shooter3BoundHpSlider.value, Time.deltaTime * 1f);
        }

    }


    public void Damage(float fDamage)
    {
        fShooter3BoundHp -= fDamage;
        Invoke("LateDamage", 0.7f);
    }


    public void LateDamage()
    {
        bLateBgHit = true;
    }
}
