using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGhost : ShooterFSM
{
    public static BombGhost Instance
    {
        get
        {
            if (BombGhostInstance == null)
            {
                BombGhostInstance = FindObjectOfType<BombGhost>();
                if (BombGhostInstance == null)
                {
                    var InstanceContainer = new GameObject("BombGhost");
                    BombGhostInstance = InstanceContainer.AddComponent<BombGhost>();
                }
            }
            return BombGhostInstance;
        }
    }
    private static BombGhost BombGhostInstance;

    public GameObject BombGhostCanavs;
    public GameObject MeleeAttack;
    public GameObject FakeBomb;
    public GameObject RealShooter;

    public float fWeaponDamge;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fBatPetWeaponDamage;
    public float fWeaponCiritical = 1.3f;

    public bool bCollisionDelay = true;


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
        
        fAttackCoolTime = 3f;
        fAttackCalculator = fAttackCoolTime;

        fAttackRange = 25f;
        fMonsterFeeling = 28f;
        fMaxHp = 1290f;
        fCurrenHp = 1290f;
        StartCoroutine(ResetAttackArea());
    }
    
    IEnumerator ResetAttackArea()
    {
        while(true)
        {
            yield return null;
            if(!MeleeAttack.activeInHierarchy && currentState == ShooterState.Attack)
            {
       
                yield return new WaitForSeconds(fAttackCoolTime);

            }


        }


    }


    protected override void InitMonster()
    {


   

    }


    private void Update()
    {
         MonsterRigidBody.velocity = Vector3.zero;
        
        if(fCurrenHp <= 0)
        {
            NaviAgent.isStopped = true;
            MonsterRigidBody.gameObject.SetActive(false);
            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);

            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;
            //Æêµé 
            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.iTargetingIndex = -1;
           
            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.iTargetingIndex = -1;



            Vector3 vCurrentPosition = new Vector3(transform.position.x, 0.5f, transform.position.y);

            for (int i = 0; i < (SceneMgr.Instance.CurrentStage / 10 + 2 * Random.Range(0f, 0.1f)); i++)
            {
                GameObject ExpClone = Instantiate(PlayerSkillData.Instance.CoinExp, vCurrentPosition, transform.rotation);
                SoundManager.Instance.MonsterSound("MonsterDropSound", SoundManager.Instance.bgList[11]);
                ExpClone.transform.parent = gameObject.transform.parent.parent;
            }


            GameObject HealthPack = Instantiate(PlayerSkillData.Instance.PlayerHealthPack, vCurrentPosition, transform.rotation);
            SoundManager.Instance.MonsterSound("MonsterDropHealthPack", SoundManager.Instance.bgList[15]);
            HealthPack.transform.parent = gameObject.transform.parent.parent;



            Destroy(transform.parent.gameObject);
            return;
        }
    }



    public void Shoot()
    {
        SoundManager.Instance.MonsterSound("MonsterBombThrow", SoundManager.Instance.bgList[31]);
        Instantiate(RealShooter, MeleeAttack.transform.position, Quaternion.identity);
    }

    public void FakeBombOff()
    {
        FakeBomb.SetActive(false);
    }

    public void FakebombOn()
    {
        FakeBomb.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Weapon"))
        {
            fWeaponDamge = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;
            GameObject DamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.Euler(90f, 0f, 0f));
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamge;
                BombGhostCanavs.GetComponent<BombGhostHpBar>().Damage(fWeaponDamge);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamge, false);
            }
            else
            {
                fCurrenHp -= fWeaponDamge * fWeaponCiritical;
                BombGhostCanavs.GetComponent<BombGhostHpBar>().Damage(fWeaponDamge * fWeaponCiritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamge * fWeaponCiritical, true);
            }
            Instantiate(EffectScript.Instance.BombGhostHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                fCurrenHp -= fBowWeaponDamage;
                BombGhostCanavs.GetComponent<BombGhostHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fBowWeaponDamage * fWeaponCiritical;
                BombGhostCanavs.GetComponent<BombGhostHpBar>().Damage(fBowWeaponDamage * fWeaponCiritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCiritical, true);

            }
            Instantiate(EffectScript.Instance.BombGhostHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }



        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fSphereWeaponDamage;
            BombGhostCanavs.GetComponent<BombGhostHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.BombGhostHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fBatPetWeaponDamage;
            BombGhostCanavs.GetComponent<BombGhostHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

    }



   



}

