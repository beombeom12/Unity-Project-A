using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSkeleton : ShooterFSM
{

    public static MagicianSkeleton Instance
    {
        get
        {
            if (MagicianInstance == null)
            {
                MagicianInstance = FindObjectOfType<MagicianSkeleton>();
                if (MagicianInstance == null)
                {
                    var InstanceContainer = new GameObject("MagicianSkeleton");
                    MagicianInstance = InstanceContainer.AddComponent<MagicianSkeleton>();
                }
            }
            return MagicianInstance;
        }
    }
    private static MagicianSkeleton MagicianInstance;

    public GameObject MagicianSkeletonCanvas;
    public Transform MeleeAttack;
    public Transform FireWallPosition;



    public GameObject FireballShot;
    public GameObject FireWall;


    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fWeaponCritical = 1.3f;
    public float fSphereWeaponDamage;
    public float fCollisionDelay = 2f;
    public float fBatPetWeaponDamage;
    public bool bCollisionDelay = true;



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fMonsterFeeling);

        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, fAttackRange);
    }



    new void Start()
    {
        base.Start();


        fAttackCoolTime = 4f;
        fAttackCalculator = fAttackCoolTime;

        fAttackRange = 15f;
        fMonsterFeeling = 10f;
        fCurrenHp = 10000f;
        fMaxHp = 10000f;

        StartCoroutine(Attack());

    }

    protected override IEnumerator Attack()
    {
        yield return null;
        NaviAgent.isStopped = true;
        transform.LookAt(Player.transform.position);

        if (Random.value < 0.5f)
        {
            if (Random.value < 0.5f)
            {
                yield return new WaitForSeconds(0.3f);
                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    Anim.SetTrigger("Attack");
                }
            }
            else
            {
  
                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
                {
                    Anim.SetTrigger("Skill");
                }
                yield return new WaitForSeconds(0.5f);
            }


        }
        else
        {

            while (true)
            {
                if (Vector3.Distance(NaviAgent.destination, transform.position) > 5f)
                {
                    if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                    {
                        Anim.SetTrigger("Run");
                    }
                }
                else
                {
                    NaviAgent.isStopped = true;
                    break;
                }
                yield return null;

            }
            NaviAgent.speed = fMoveSpeed;
            NaviAgent.stoppingDistance = fAttackRange;
        }
        bIsMonsterAttack = false;
        currentState = ShooterState.Idle;
    }







    // Update is called once per frame
    void Update()
    {
        MonsterRigidBody.velocity = Vector3.zero;

        if(fCurrenHp <= 0)
        {
            fCurrenHp = 0;
            Instantiate(EffectScript.Instance.SkeletonDeadEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            NaviAgent.isStopped = true;
            MonsterRigidBody.gameObject.SetActive(false);

            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;
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
        SoundManager.Instance.MonsterSound("MagicianSkeletonFireBall", SoundManager.Instance.bgList[37]);
        Instantiate(FireballShot, MeleeAttack.position, Quaternion.Euler(transform.eulerAngles +new Vector3(- 90f, 0f, 0f)));
    }

    public void FireWallShot()
    {

        Instantiate(FireWall, new Vector3(FireWallPosition.transform.position.x, FireWallPosition.transform.position.y, FireWallPosition.transform.position.z), transform.rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;

            GameObject DamageTextClone = Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamage;
                MagicianSkeletonCanvas.GetComponent<MagicianSkeletonHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);

            }
            else
            {
                fCurrenHp -= fWeaponDamage * fWeaponCritical;
                MagicianSkeletonCanvas.GetComponent<MagicianSkeletonHpBar>().Damage(fWeaponCritical * fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);


            }
            Instantiate(EffectScript.Instance.MagicianSkeletonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
               MagicianSkeletonCanvas.GetComponent<MagicianSkeletonHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fBowWeaponDamage * fWeaponCritical;
                MagicianSkeletonCanvas.GetComponent<MagicianSkeletonHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.MagicianSkeletonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }




        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fSphereWeaponDamage;
            MagicianSkeletonCanvas.GetComponent<MagicianSkeletonHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.MagicianSkeletonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fBatPetWeaponDamage;
            MagicianSkeletonCanvas.GetComponent<MagicianSkeletonHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }





}
