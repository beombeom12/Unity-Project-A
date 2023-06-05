using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDragon : ShooterFSM
{
    public static MiniDragon Instance
    {
        get
        {
            if (MiniDragonInstance == null)
            {
                MiniDragonInstance = FindObjectOfType<MiniDragon>();
                if (MiniDragonInstance == null)
                {
                    var InstanceContainer = new GameObject("MiniDragon");
                    MiniDragonInstance = InstanceContainer.AddComponent<MiniDragon>();
                }
            }
            return MiniDragonInstance;
        }
    }
    private static MiniDragon MiniDragonInstance;

    public GameObject MiniDrageCanvase;
    public GameObject MeleeAttack;
    public GameObject RealFireShoot;

    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fBatPetWeaponDamage;
    public float fWeaponCritical = 1.3f;

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
        fMonsterFeeling = 25f;
        fMaxHp = 1600f;
        fCurrenHp = 1600f;

        StartCoroutine(ResetAttackArea());
        
    }


   IEnumerator ResetAttackArea()
    {
        while(true)
        {
            yield return null;
            if (!MeleeAttack.activeInHierarchy && currentState == ShooterState.Attack)
            {

                yield return new WaitForSeconds(fAttackCoolTime);
                
            }
        }
    }



    protected override void InitMonster()
    {
    }

    public void MiniDragonshoot()
    {
        SoundManager.Instance.MonsterSound("MiniDragonAttacking", SoundManager.Instance.bgList[38]);
        Instantiate(RealFireShoot, MeleeAttack.transform.position, transform.rotation);
    }


    private void Update()
    {
        MonsterRigidBody.velocity = Vector3.zero;

        if(fCurrenHp <= 0)
        {
            NaviAgent.isStopped = true;
            MonsterRigidBody.gameObject.SetActive(false);

            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f,0f,0f));


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
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;
            GameObject DamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.Euler(90f, 0f, 0f));
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamage;
                MiniDrageCanvase.GetComponent<MiniDragonHp>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fWeaponDamage * fWeaponCritical;
                MiniDrageCanvase.GetComponent<MiniDragonHp>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);
            }
            Instantiate(EffectScript.Instance.MiniDragonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                MiniDrageCanvase.GetComponent<MiniDragonHp>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fBowWeaponDamage * fWeaponCritical;
                MiniDrageCanvase.GetComponent<MiniDragonHp>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.MiniDragonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }



        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fSphereWeaponDamage;
            MiniDrageCanvase.GetComponent<MiniDragonHp>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.MiniDragonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fBatPetWeaponDamage;
            MiniDrageCanvase.GetComponent<MiniDragonHp>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

    }


}
