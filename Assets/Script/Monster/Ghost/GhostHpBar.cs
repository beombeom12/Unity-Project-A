using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostHpBar : MonoBehaviour
{
    public static GhostHpBar Instance
    {
        get
        {
            if (GhostInstnace == null)
            {
                GhostInstnace = FindObjectOfType<GhostHpBar>();
                if (GhostInstnace == null)
                {
                    var InstanceContainer = new GameObject("GhostHpBar");
                    GhostInstnace = InstanceContainer.AddComponent<GhostHpBar>();
                }
            }
            return GhostInstnace;
        }
    }



    private static  GhostHpBar GhostInstnace;

    
    public Slider GhostHpSlider;
    public Slider LateHpSlider;

    public Transform Ghost1;
   
    public float m_fMaxHp = 1000f;
    public float m_fCurrentHp = 1000f;

    public bool bLateBackHit = false;



  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {

        transform.position = new Vector3(Ghost1.transform.position.x, Ghost1.transform.position.y + 1f, Ghost1.transform.position.z);

        GhostHpSlider.value = Mathf.Lerp(GhostHpSlider.value, m_fCurrentHp / m_fMaxHp, Time.deltaTime * 5f);


        if(bLateBackHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, GhostHpSlider.value, Time.deltaTime * 1f);
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
