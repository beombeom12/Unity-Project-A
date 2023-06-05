using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake: GhostFSM 
{


    public static Snake Instance
    {
        get
        {
            if (SnakeInstance == null)
            {
                SnakeInstance = FindObjectOfType<Snake>();
                if (SnakeInstance == null)
                {
                    var InstanceContainer = new GameObject("Snake");
                    SnakeInstance = InstanceContainer.AddComponent<Snake>();
                }
            }
            return SnakeInstance;
        }
    }

    private static Snake SnakeInstance;



    public GameObject SnakeCanavas;
    public GameObject MeleeAttack;


    //플레이어에게 맞는 데미지 표시하기
    public float fWeaponDamaged;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamaged;
    public float fWeaponCritical = 1.3f;
    public float fBatPetWeaponDamage;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fMonsterFeeling);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, fAttackRange);
    }


    new void Start()
    {
        base.Start();
        fAttackCoolTime = 4f;
        fAttackCalculator = fAttackCoolTime;

        fAttackRange = 5f;
        NaviAgent.stoppingDistance = 0.1f;


        StartCoroutine(ResetAttack());
        
    }


    IEnumerator ResetAttack()
    {
        while(true)
        {
            yield return null;

            if (!MeleeAttack.activeInHierarchy && currentState == GhostState.Attack)
            {
                yield return new WaitForSeconds(fAttackCoolTime);

                if (currentState == GhostState.Attack)
                {

                    
                    MeleeAttack.SetActive(true);

                }
            }            
        }
    }



    protected override void InitMonster()
    {

        fMaxHp = 1500f;
        fCurrenHp = fMaxHp;

        MonsterDamage = 150f;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(fCurrenHp <= 0)
        {
            fCurrenHp = 0;
            NaviAgent.isStopped = true;
            MonsterRigidBody.gameObject.SetActive(false);

            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));

            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;
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




            MonsterRigidBody.velocity = Vector3.zero;
            Destroy(transform.parent.gameObject);
            return;

        }



    }

    public void MeleeAttackOn()
    {
        SoundManager.Instance.MonsterSound("SnakeAttackSound", SoundManager.Instance.bgList[35]);
        MeleeAttack.SetActive(true);
    }
    public void MeleeAttackOff()
    {
        MeleeAttack.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Weapon"))
        {
            fWeaponDamaged = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;

            GameObject DamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            if (Random.value < 0.6)
            {
                fCurrenHp -= fWeaponDamaged;
              
                SnakeCanavas.GetComponent<SnakeHpBar>().Damage(fWeaponDamaged);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamaged, false);
            }
            else
            {
                fCurrenHp -= fWeaponDamaged * 1.3f;
                
                SnakeCanavas.GetComponent<SnakeHpBar>().Damage(fWeaponDamaged * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamaged * fWeaponCritical, true);
            }
            Instantiate(EffectScript.Instance.SnakeHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                SnakeCanavas.GetComponent<SnakeHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fCurrenHp -= fBowWeaponDamage * fWeaponCritical;
                SnakeCanavas.GetComponent<SnakeHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.SnakeHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamaged = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
      
                fCurrenHp -= fSphereWeaponDamaged;
            SnakeCanavas.GetComponent<SnakeHpBar>().Damage(fSphereWeaponDamaged);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamaged, false);
            Instantiate(EffectScript.Instance.SnakeHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fCurrenHp -= fBatPetWeaponDamage;
            SnakeCanavas.GetComponent<SnakeHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

    }


 


    
}
