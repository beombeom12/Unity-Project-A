using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkeleton : GhostFSM
{
    public static WarriorSkeleton Instance
    {
        get
        {
            if (WarriorSkeletonInstance == null)
            {
                WarriorSkeletonInstance = FindObjectOfType<WarriorSkeleton>();
                if (WarriorSkeletonInstance == null)
                {
                    var InstanceContainer = new GameObject("WarriorSkeleton");
                    WarriorSkeletonInstance = InstanceContainer.AddComponent<WarriorSkeleton>();
                }
            }
            return WarriorSkeletonInstance;
        }
    }
    private static WarriorSkeleton WarriorSkeletonInstance;


    public GameObject WarriorSkeletonCanvas;
    public GameObject MeleeAttack;

    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float CollisionDelay = 2f;
    public float fBatPetWeaponDamage;
    public float fWeaponCritical = 1.3f;
    

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
        fCurrenHp = 20000f;
        fMaxHp = 20000f;
        fAttackCoolTime = 3f;
        fAttackCalculator = fAttackCoolTime;
        fMonsterFeeling = 15f;
        fAttackRange = 5f;
        NaviAgent.stoppingDistance = 1f;
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
    // Update is called once per frame
    void Update()
    {
        if(fCurrenHp <= 0)
        {
            fCurrenHp = 0;
            NaviAgent.isStopped = true;
            Instantiate(EffectScript.Instance.SkeletonDeadEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            MonsterRigidBody.gameObject.SetActive(false);

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


            GameObject HealthPack = Instantiate(PlayerSkillData.Instance.PlayerHealthPack, vCurrentPosition, transform.rotation);
            SoundManager.Instance.MonsterSound("MonsterDropHpSound", SoundManager.Instance.bgList[15]);
            HealthPack.transform.parent = gameObject.transform.parent.parent;



            Destroy(transform.parent.gameObject);
        
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;

            GameObject DamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamage;
                WarriorSkeletonCanvas.GetComponent<WarriorSkeletonHpbar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {

                fCurrenHp -= fWeaponDamage * fWeaponCritical;
                WarriorSkeletonCanvas.GetComponent<WarriorSkeletonHpbar>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);
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
                WarriorSkeletonCanvas.GetComponent<WarriorSkeletonHpbar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fBowWeaponDamage * fWeaponCritical;
                WarriorSkeletonCanvas.GetComponent<WarriorSkeletonHpbar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

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
            WarriorSkeletonCanvas.GetComponent<WarriorSkeletonHpbar>().Damage(fSphereWeaponDamage);
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
            WarriorSkeletonCanvas.GetComponent<WarriorSkeletonHpbar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }








    public void MeleeAttacking()
    {
        SoundManager.Instance.MonsterSound("WarriorSound", SoundManager.Instance.bgList[42]);
        MeleeAttack.SetActive(true);
    }
    public void MeleeAttackOff()
    {
        MeleeAttack.SetActive(false);
    }

    public void SwordTrail()
    {
        Instantiate(EffectScript.Instance.SwordTrailEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
    }    
}
