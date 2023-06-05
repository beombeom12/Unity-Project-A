using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : GhostFSM
{
    public static  Ghost Instance
    {
        get
        {
            if (GhostInstance == null)
            {
                GhostInstance = FindObjectOfType<Ghost>();
                if (GhostInstance == null)
                {
                    var InstanceContainer = new GameObject("Ghost");
                    GhostInstance = InstanceContainer.AddComponent<Ghost>();
                }
            }
            return GhostInstance;
        }
    }



    private static Ghost GhostInstance;


    public GameObject GhostCanvase;
    public GameObject MeleeAttack;
   
    
    
    public float fWeaponDamage;
    public float fSphereWeaponDamage;
    public float fBatPetWeaponDamage;
    public float fBowDamage;


    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fMonsterFeeling);

        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, fAttackRange);
    }



    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        fAttackCoolTime = 2f;
        fAttackCalculator = fAttackCoolTime;

        fAttackRange = 5f;
        NaviAgent.stoppingDistance = 0.1f;

        StartCoroutine(ResetAttackArea());
    }


    IEnumerator ResetAttackArea()
    {
        while(true)
        {
            yield return null;
            if(!MeleeAttack.activeInHierarchy && currentState == GhostState.Attack)
            {
                yield return new WaitForSeconds(fAttackCoolTime);
             
            }
        }
    }


    protected override void InitMonster()
    {
        fMaxHp += (SceneMgr.Instance.CurrentStage);
        fCurrenHp = fMaxHp;

        MonsterDamage += (SceneMgr.Instance.CurrentStage);

    }

    protected override void AttackEffect()
    {
        
    }



    public void MeleeAttackOn()
    {
        SoundManager.Instance.MonsterSound("GhostBiteSound", SoundManager.Instance.bgList[33]);
        MeleeAttack.SetActive(true);

    }

    public void MeleeAttackOff()
    {
        MeleeAttack.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(fCurrenHp <= 0)
        {
            NaviAgent.isStopped = true;

            MonsterRigidBody.gameObject.SetActive(false);

      
            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;
            PetTargeting.Instance.iTargetingIndex = -1;
            BatPetTargeting.Instance.iTargetingIndex = -1;
            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));

            Vector3 vCurrentPosition = new Vector3(transform.position.x, 0.8f, transform.position.z);

            for (int i = 0; i < (SceneMgr.Instance.CurrentStage / 10 + 2 + Random.Range(0f, 0.1f)); i++)
            {
                GameObject ExpClone = Instantiate(PlayerSkillData.Instance.CoinExp, vCurrentPosition, transform.rotation);
                SoundManager.Instance.MonsterSound("MonsterDropSound", SoundManager.Instance.bgList[11]);
                ExpClone.transform.parent = gameObject.transform.parent.parent;                            
            }

            //헬스팩을 하나만 생성을 한다. 
            GameObject HealPack = Instantiate(PlayerSkillData.Instance.PlayerHealthPack, vCurrentPosition, transform.rotation);
            SoundManager.Instance.MonsterSound("MonsterDropHpSound", SoundManager.Instance.bgList[15]);
            HealPack.transform.parent = gameObject.transform.parent.parent;

        
            MonsterRigidBody.velocity = Vector3.zero;


            Destroy(transform.parent.gameObject);
            return;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage= other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;

            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            GameObject DamageTextClone =
            Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamage;
                GhostCanvase.GetComponent<GhostHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
             
            }
            else
            {
                fCurrenHp -= fWeaponDamage * 1.3f;
                GhostCanvase.GetComponent<GhostHpBar>().Damage(fWeaponDamage * 1.3f);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * 1.3f, true);

            }
            Instantiate(EffectScript.Instance.GhostHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if(other.transform.CompareTag("BowWeapon"))
        {
            fBowDamage = other.gameObject.GetComponent<PlayerBowShooter>().fDamage;
            Instantiate(EffectScript.Instance.BowHittingEffect, transform.position, Quaternion.identity);
            GameObject BowDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            if(Random.value < 0.6)
            {
                fCurrenHp -= fBowDamage;
                GhostCanvase.GetComponent<GhostHpBar>().Damage(fBowDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowDamage, false);
            }
            else
            {
                fCurrenHp -= fBowDamage;
                GhostCanvase.GetComponent<GhostHpBar>().Damage(fBowDamage * 1.3f);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowDamage * 1.3f, true);
            }
            Instantiate(EffectScript.Instance.GhostHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }




        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
      
                fCurrenHp -= fSphereWeaponDamage;
                GhostCanvase.GetComponent<GhostHpBar>().Damage(fSphereWeaponDamage);
                SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.GhostHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fBatPetWeaponDamage;
            GhostCanvase.GetComponent<GhostHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }




    
}
