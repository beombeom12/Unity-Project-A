using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : GhostFSM
{

    public GameObject BossBolt;

   
    public Transform AttackPointer;
    public Transform RightArmAtt;
    public LayerMask layerMaskWall;

    WaitForSeconds Delay50 = new WaitForSeconds(0.05f);
    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);

    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fCriticalDamage = 1.3f;
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
        fAttackCoolTime = 1.5f;
        fAttackCalculator = fAttackCoolTime;

        fMonsterFeeling = 8f;
        fAttackRange = 20f;
        fMoveSpeed = 1f;
        NaviAgent.stoppingDistance = 4f;
    }


    protected override void InitMonster()
    {
        fMaxHp = 100000f;
        fCurrenHp = fMaxHp;
        MonsterDamage = 300f;
        StartCoroutine(Attack());
    }


    protected override IEnumerator Attack()
    {
        yield return null;
        NaviAgent.isStopped = true;
        transform.LookAt(Player.transform.position);

        if (Random.value < 0.6)
        {
            if (Random.value < 0.3f)
            {
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
            }
            yield return Delay50;

        }
        else
        {
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Call"))
            {
                Anim.SetTrigger("Hitted");
               
            }
            Instantiate(EffectScript.Instance.BossHittedEffect, transform.position, Quaternion.identity);
            transform.LookAt(Player.transform.position);
            Physics.Raycast(new Vector3(transform.position.x, 0f, transform.position.z), transform.forward, out RaycastHit hit, 60f, layerMaskWall);
            Vector3 targetPoint = hit.point;

            yield return Delay500;

            NaviAgent.isStopped = false;
            NaviAgent.stoppingDistance = 0f;
            NaviAgent.SetDestination(Player.transform.position);
            NaviAgent.speed = 150f;



            while (true)
            {

                if (Vector3.Distance(NaviAgent.destination, transform.position) > 3f)
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
        currentState = GhostState.Idle;

    }



    public void BossAttack()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -35f, 0f)));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f, 0f)));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 35f, 0f)));
    }


    public void PutyourHandsUp()
    {
        Instantiate(EffectScript.Instance.LastBossPutYourHandsUp, new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
        SoundManager.Instance.MonsterSound("MonsterSound", SoundManager.Instance.bgList[29]);

    }
    public void BossSkill()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f, 0f)));

    }

    public void BossSkill1()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 60f, 0f)));
    }
    public void BossSkill2()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 120f, 0f)));
    }
    public void BossSkill3()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 180f, 0f)));
    }
    public void BossSkill4()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 240f, 0f)));
    }
    public void BossSkill5()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 300f, 0f)));
    }
    public void BossSkill6()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 30f, 0f)));
    }
    public void BossSkill7()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 90, 0f)));
    }
    public void BossSkill8()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 150f, 0f)));
    }
    public void BossSkill9()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 210f, 0f)));
    }
    public void BossSkill10()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 330f, 0f)));
    }
    public void BossSkill11()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 360f, 0f)));
    }
    public void BossSkill112()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 390f, 0f)));
    }

    public void BossSkill13()
    {
        SoundManager.Instance.MonsterSound("LastBossBoltThrow", SoundManager.Instance.bgList[28]);
        Instantiate(EffectScript.Instance.LastBossTrailEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(EffectScript.Instance.LastBossTrailEffect2, transform.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(BossBolt, AttackPointer.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 390f, 0f)));
    }
    private void Update()
    {
        if(fCurrenHp <= 0)
        {



            Instantiate(EffectScript.Instance.SkeletonDeadEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            SoundManager.Instance.MonsterSound("LastBossDead", SoundManager.Instance.bgList[27]);
            NaviAgent.isStopped = true;
            MonsterRigidBody.gameObject.SetActive(false);
            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;
            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.iTargetingIndex = -1;
            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.iTargetingIndex = -1;
            UIGameController.Instance.CheckBossRoom(false);


            Destroy(transform.gameObject);
            return;
        }
        else
        {
            UIGameController.Instance.fBossCurrentHp = fCurrenHp;
            UIGameController.Instance.fBossMaxHp = fMaxHp;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            GameObject DamageTextClone =
            Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamage;
                UIGameController.Instance.Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fWeaponDamage * fCriticalDamage;
                UIGameController.Instance.Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fCriticalDamage, true);
            }
            Instantiate(EffectScript.Instance.LastBossHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                UIGameController.Instance.Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fBowWeaponDamage * fCriticalDamage;
                UIGameController.Instance.Damage(fBowWeaponDamage * fCriticalDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fCriticalDamage, true);

            }
            Instantiate(EffectScript.Instance.LastBossHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }




        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fSphereWeaponDamage;
            UIGameController.Instance.Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.LastBossHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fBatPetWeaponDamage;
            UIGameController.Instance.Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }



}

