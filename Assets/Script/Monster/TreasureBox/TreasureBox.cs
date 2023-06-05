using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public static TreasureBox Instance
    {
        get
        {
            if (TreasureBoxInstance == null)
            {
                TreasureBoxInstance = FindObjectOfType<TreasureBox>();
                if (TreasureBoxInstance == null)
                {
                    var InstanceContainer = new GameObject("TreasureBox");
                    TreasureBoxInstance = InstanceContainer.AddComponent<TreasureBox>();
                }
            }
            return TreasureBoxInstance;
        }
    }
    private static TreasureBox TreasureBoxInstance;

    RoomChecker roomIsChecker;

    Animator Anim;


    public GameObject Player;
    public GameObject TreasureBoxCanvas;



    public float fTreasureBoxHp = 5000f;
    public float fTreasureBoxMaxHp = 5000f;


    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fWeaponCritical = 1.3f;

    public float fCollisionDelay = 2f;
    public bool bCollisionDelay = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Anim = GetComponent<Animator>();

        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();


        if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Anim.SetTrigger("Idle");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(fTreasureBoxHp <= 0)
        {

            fTreasureBoxHp = 0;
            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;


            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.iTargetingIndex = -1;

            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.iTargetingIndex = -1;

            Vector3 vCurrentPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
            for (int i = 0; i < (SceneMgr.Instance.CurrentStage / 10 + 2 + Random.Range(0f, 0.1f)); i++)
            {
                GameObject ExpClone = Instantiate(PlayerSkillData.Instance.CoinExp, vCurrentPosition, transform.rotation);
                SoundManager.Instance.MonsterSound("MonsterDropSound", SoundManager.Instance.bgList[11]);
                ExpClone.transform.parent = gameObject.transform.parent.parent;
            }

            //이안에 코인과 주문서들을 드랍시킬것.

            Destroy(transform.parent.gameObject);
        }
        
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;

            GameObject DamageTextClone = Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            if (Random.value < 0.6)
            {
                fTreasureBoxHp -= fWeaponDamage;
                SoundManager.Instance.MonsterSound("TreausreboxSound", SoundManager.Instance.bgList[40]);
                TreasureBoxCanvas.GetComponent<TreasureBoxHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);


            }
            else
            {
                fTreasureBoxHp -= fWeaponDamage * fWeaponCritical;
                SoundManager.Instance.MonsterSound("TreausreboxSound", SoundManager.Instance.bgList[40]);
                TreasureBoxCanvas.GetComponent<TreasureBoxHpBar>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);


            }
            Instantiate(EffectScript.Instance.TreasureBoxHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }



        if (other.gameObject.CompareTag("BowWeapon"))
        {
            fBowWeaponDamage = other.gameObject.GetComponent<PlayerBowShooter>().fDamage;

            Instantiate(EffectScript.Instance.BowHittingEffect, transform.position, Quaternion.identity);
            GameObject BowDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);


            if (Random.value < 0.6)
            {
                fTreasureBoxHp -= fBowWeaponDamage;
                SoundManager.Instance.MonsterSound("TreausreboxSound", SoundManager.Instance.bgList[40]);
                TreasureBoxCanvas.GetComponent<TreasureBoxHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fTreasureBoxHp -= fBowWeaponDamage * fWeaponCritical;
                SoundManager.Instance.MonsterSound("TreausreboxSound", SoundManager.Instance.bgList[40]);
                TreasureBoxCanvas.GetComponent<TreasureBoxHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.TreasureBoxHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }



        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fTreasureBoxHp -= fSphereWeaponDamage;
            SoundManager.Instance.MonsterSound("TreausreboxSound", SoundManager.Instance.bgList[40]);
            TreasureBoxCanvas.GetComponent<TreasureBoxHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.TreasureBoxHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }



    
}
