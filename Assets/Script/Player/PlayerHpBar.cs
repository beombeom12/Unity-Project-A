using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class PlayerHpBar : MonoBehaviour
{



    public static PlayerHpBar Instance
    {
        get
        {
            if (playerHpBarInstance == null)
            {
                playerHpBarInstance = FindObjectOfType<PlayerHpBar>();
                if (playerHpBarInstance == null)
                {
                    var InstanceContainer = new GameObject("PlayerHpBar");
                    playerHpBarInstance = InstanceContainer.AddComponent<PlayerHpBar>();
                }
            }
            return playerHpBarInstance;
        }
    }



    private static PlayerHpBar playerHpBarInstance;



    public Transform player;

    public Slider HudHpBar;
    public Slider LateHudHpBar;


    public Text HpTExt;

    public bool bLateHudBar = false;

    public float m_fMaxHp;
    public float m_fCurrentHp ;





    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {



        HpTExt.text = "" +  PlayerSkillData.Instance.fPlayerCurrentHp;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);


        HudHpBar.value = Mathf.Lerp(HudHpBar.value, PlayerSkillData.Instance.fPlayerCurrentHp / PlayerSkillData.Instance.fPlayerMaxHp, Time.deltaTime * 5f);

        if(bLateHudBar)
        {
            LateHudHpBar.value = Mathf.Lerp(LateHudHpBar.value, HudHpBar.value, Time.deltaTime * 0.8f);
        }

        if (m_fCurrentHp <= 0f)
        {
            m_fCurrentHp = 0f;
        }
    }

    public void TakeDamaged(float fDamage)
    {
        m_fCurrentHp -= fDamage;

    

        Invoke("LateDamage", 0.7f);


    }


    public void LateDamage()
    {
        bLateHudBar = true;
    }







}
