using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHpBar : MonoBehaviour
{
    public static SnakeHpBar Instance
    {
        get
        {
            if (SnakeHpBarInstance == null)
            {
                SnakeHpBarInstance = FindObjectOfType<SnakeHpBar>();
                if (SnakeHpBarInstance == null)
                {
                    var InstanceContainer = new GameObject("SnakeHpBar");
                    SnakeHpBarInstance = InstanceContainer.AddComponent<SnakeHpBar>();
                }
            }
            return SnakeHpBarInstance;
        }
    }
    private static SnakeHpBar SnakeHpBarInstance;

    public Slider SnakeHpSlider;
    public Slider LateHpSlider;

    public Transform Snaketransform;

    public float fHp = 1290f;
    public float fMaxHp = 1290f;

    public bool bLateHitting = false;


    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(Snaketransform.transform.position.x, Snaketransform.transform.position.y + 1f, Snaketransform.transform.position.z);

        SnakeHpSlider.value = Mathf.Lerp(SnakeHpSlider.value, fHp / fMaxHp, Time.deltaTime * 0.78f);


        if (bLateHitting)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, SnakeHpSlider.value, Time.deltaTime * 0.78f);
        }

    }

    public void Damage(float fDamage)
    {
        fHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDamage()
    {
        bLateHitting = true;
    }

}
