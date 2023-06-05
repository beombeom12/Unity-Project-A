using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSkeleton : GhostFSM
{
    public static CloseSkeleton Instance
    {
        get
        {
            if (CloseSkeletonInstance == null)
            {
                CloseSkeletonInstance = FindObjectOfType<CloseSkeleton>();
                if (CloseSkeletonInstance == null)
                {
                    var InstanceContainer = new GameObject("CloseSkeleton");
                    CloseSkeletonInstance = InstanceContainer.AddComponent<CloseSkeleton>();
                }
            }
            return CloseSkeletonInstance;
        }
    }
    private static CloseSkeleton CloseSkeletonInstance;

    public GameObject CloseSkeletonCanvase;
    public GameObject MeleeAttack;

    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fWeaponCritical = 1.3f;
    public float fBatPetWeaponDamage;


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
        fCurrenHp = 2300f;
        fMaxHp = 2300f;
        fAttackCoolTime = 2f;
        fAttackCalculator = fAttackCoolTime;
        fMonsterFeeling = 15f;
        fAttackRange = 4f;
        NaviAgent.stoppingDistance = 4f;
        StartCoroutine(ResetAttackArea());
    }


     IEnumerator ResetAttackArea()
    {
        while (true)
        {
            yield return null;
            NaviAgent.speed = 4f;
            NaviAgent.stoppingDistance = 4f;
            if (!MeleeAttack.activeInHierarchy && currentState == GhostState.Attack)
            {
                NaviAgent.stoppingDistance = 4f;
                NaviAgent.speed = 0f;
                yield return new WaitForSeconds(fAttackCoolTime);

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (fCurrenHp <= 0)
        {
            fCurrenHp = 0;
            NaviAgent.isStopped = true;
            Instantiate(EffectScript.Instance.SkeletonDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            MonsterRigidBody.gameObject.SetActive(false);




            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;



            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.iTargetingIndex = -1;


            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.iTargetingIndex = -1;

            Vector3 vCurrentPosition = new Vector3(transform.position.x, 0.5f, transform.position.y);

            for(int i = 0; i < (SceneMgr.Instance.CurrentStage / 10 + 2 * Random.Range(0f, 0.1f)); i++)
            {
                GameObject ExpClone = Instantiate(PlayerSkillData.Instance.CoinExp, vCurrentPosition, transform.rotation);
                SoundManager.Instance.MonsterSound("MonsterDropSound", SoundManager.Instance.bgList[11]);
                ExpClone.transform.parent = gameObject.transform.parent.parent;
            }


            GameObject HealthPack = Instantiate(PlayerSkillData.Instance.PlayerHealthPack, vCurrentPosition, transform.rotation);
            SoundManager.Instance.MonsterSound("MonsterDropHealthPack", SoundManager.Instance.bgList[15]);
            HealthPack.transform.parent = gameObject.transform.parent.parent;


            Destroy(transform.parent.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;

            GameObject DamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));

            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamage;
                CloseSkeletonCanvase.GetComponent<CloseSkeletonHp>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {

                fCurrenHp -= fWeaponDamage * fWeaponCritical;
                CloseSkeletonCanvase.GetComponent<CloseSkeletonHp>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);
            }
            Instantiate(EffectScript.Instance.CloseSkeletonEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
            return;
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
                CloseSkeletonCanvase.GetComponent<CloseSkeletonHp>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fBowWeaponDamage * fWeaponCritical;
                CloseSkeletonCanvase.GetComponent<CloseSkeletonHp>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.CloseSkeletonEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));

            Destroy(other.gameObject);
        }



        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fSphereWeaponDamage;
            CloseSkeletonCanvase.GetComponent<CloseSkeletonHp>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.CloseSkeletonEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));

            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fBatPetWeaponDamage;
            CloseSkeletonCanvase.GetComponent<CloseSkeletonHp>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }



    public void ColliderOn()
    {
        SoundManager.Instance.MonsterSound("CloseSkeletonSound", SoundManager.Instance.bgList[36]);
        MeleeAttack.SetActive(true);
    }

    public void ColliderOff()
    {
        MeleeAttack.SetActive(false);
    }

    public void SwordTrail()
    {
        Instantiate(EffectScript.Instance.SmallSkeletonTrailEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
    }
}
