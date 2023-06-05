using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArcheroHpBar : MonoBehaviour
{

    public static ArcheroHpBar Instance
    {
        get
        {
            if (ArcheroHpInstance == null)
            {
                ArcheroHpInstance = FindObjectOfType<ArcheroHpBar>();
                if (ArcheroHpInstance == null)
                {
                    var InstanceContainer = new GameObject("ArcheroHpBar");
                    ArcheroHpInstance = InstanceContainer.AddComponent<ArcheroHpBar>();
                }
            }
            return ArcheroHpInstance;
        }
    }
    private static ArcheroHpBar ArcheroHpInstance;


    public Slider ArcheroMHpSlider;
    public Slider ArcheroMLateHpSlider;

    public Transform ArcherMonster;

    public float fArcherHp = 1190f;
    public float fArcherMaxHp = 1190f;


    public bool bLateBackHit = false;

    // Start is called before the first frame update
    void Update()
    {

        transform.position = new Vector3(ArcherMonster.transform.position.x, ArcherMonster.transform.position.y + 0.7f, ArcherMonster.transform.position.z);


        ArcheroMHpSlider.value = Mathf.Lerp(ArcheroMHpSlider.value, fArcherHp / fArcherMaxHp, Time.deltaTime * 5f);

        if(bLateBackHit)
        {
            ArcheroMLateHpSlider.value = Mathf.Lerp(ArcheroMLateHpSlider.value, ArcheroMHpSlider.value, Time.deltaTime * 1f);
        }
    }


    public void Damage(float fDamage)
    {
        fArcherHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDamage()
    {
        bLateBackHit = true;
    }

        
}
