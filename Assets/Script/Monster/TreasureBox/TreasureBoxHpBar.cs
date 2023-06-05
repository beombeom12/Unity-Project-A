using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TreasureBoxHpBar : MonoBehaviour
{



    private static TreasureBoxHpBar TreasureBoxInstance;


    public Slider TreasureBoxHpSlider;
    public Slider LateHpSlider;

    public Transform treasurebox;

    public float fTreasureBoxHp = 5000f;
    public float fTreausreBoxMaxHp = 5000f;


    public bool bLateHit = false;

    // Start is called before the first frame update
    void Update()
    {

        transform.position = treasurebox.position;


        TreasureBoxHpSlider.value = Mathf.Lerp(TreasureBoxHpSlider.value, fTreasureBoxHp / fTreausreBoxMaxHp, Time.deltaTime * 5f);

        if(bLateHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, TreasureBoxHpSlider.value, Time.deltaTime * 1f);
        }



    }


    public void Damage(float fDamage)
    {
        fTreasureBoxHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDamage()
    {
        bLateHit = true;
    }

  
}
