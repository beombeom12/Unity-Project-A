using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkeletonHpBar : MonoBehaviour
{

    public static SkeletonHpBar Instance
    {
        get
        {
            if (SkeletonHpBarInstance == null)
            {
                SkeletonHpBarInstance = FindObjectOfType<SkeletonHpBar>();
                if (SkeletonHpBarInstance == null)
                {
                    var InstanceContainer = new GameObject("SkeletonHpBar");
                    SkeletonHpBarInstance = InstanceContainer.AddComponent<SkeletonHpBar>();
                }
            }
            return SkeletonHpBarInstance;
        }
    }
    private static SkeletonHpBar SkeletonHpBarInstance;

    public Slider SkeletonHpSlider;
    public Slider LateHpSlider;

 //   public Transform skletonPosition;

    public float fSkeletonHp;
    public float fSkeletonMaxHp;

    public bool bLatehit = false;
    public void Start()
    {
        fSkeletonHp = Skeleton.Instance.fSkeletonHp;
        fSkeletonMaxHp = Skeleton.Instance.fSkeletonMaxHp;
    }


    // Start is called before the first frame update
    void Update()
    {

       
        SkeletonHpSlider.value = Mathf.Lerp(SkeletonHpSlider.value, fSkeletonHp / fSkeletonMaxHp, Time.deltaTime * 5f);


        if(bLatehit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, SkeletonHpSlider.value, Time.deltaTime);
        }
   

    }

    public void Damage(float fDamage)
    {
        fSkeletonHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDamage()
    {
        bLatehit = true;
    }




}
