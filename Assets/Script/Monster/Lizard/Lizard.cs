using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public static Lizard Instance
    {
        get
        {
            if (LizardInstance == null)
            {
                LizardInstance = FindObjectOfType<Lizard>();
                if (LizardInstance == null)
                {
                    var InstanceContainer = new GameObject("Lizard");
                    LizardInstance = InstanceContainer.AddComponent<Lizard>();
                }
            }
            return LizardInstance;
        }
    }


    private static Lizard LizardInstance;


    RoomChecker roomIsChecker;

    public GameObject Player;
    public GameObject LizardCanvas;

    public Transform shooterPosition;

    


     Animator anim; 

    public float fLizardHp = 1500f;
    public float fLizardMaxHp = 1500f;
    public float RotationSpeed = 100f;

    float fWeaponDamage;
    float fBowWeaponDamage;
    float fSphereWeaponDamage;
    public float fBatPetWeaponDamage;
    public float fWeaponCritical = 1.3f;

    public float fCollisionDelay = 2f;

    public bool bCollisionDelay = true;

    Vector3 RenewalPosition;



     void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");


        anim = GetComponent<Animator>();

        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Idle");
        }

        StartCoroutine(ReadyForPlayer());

    }


    private void Update()
    {
        if(fLizardHp <= 0)
        {

            fLizardHp = 0;

            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));
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

    IEnumerator ReadyForPlayer()
    {
        yield return null;

        while(!roomIsChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.5f);
        }
        while(true)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.SetTrigger("Idle");
            }
            yield return new WaitForSeconds(3.5f);

  
            ResetAttack();

            yield return new WaitForSeconds(1.3f);
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.SetTrigger("Attack");
      
            }

        }
    }



    public void ResetAttack()
    {
        transform.LookAt(Player.transform.position);

        Vector3 vDirection = Player.transform.position - transform.position;
        vDirection.y = 0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vDirection), 1f * Time.deltaTime);
    }


    public void FireAttack()
    {
        SoundManager.Instance.MonsterSound("LizardSound", SoundManager.Instance.bgList[34]);
        RenewalPosition = new Vector3(shooterPosition.position.x, shooterPosition.position.y, shooterPosition.position.z);
        Instantiate(EffectScript.Instance.LizardWeaponEffect, RenewalPosition, transform.rotation);
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
                fLizardHp -= fWeaponDamage;
                LizardCanvas.GetComponent<LizardHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);

            }
            else
            {
                fLizardHp -= fWeaponDamage * fWeaponCritical;

                LizardCanvas.GetComponent<LizardHpBar>().Damage(fWeaponDamage * fWeaponCritical);

                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, true);
            }
            Instantiate(EffectScript.Instance.LizardHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                fLizardHp -= fBowWeaponDamage;
                LizardCanvas.GetComponent<LizardHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fLizardHp -= fBowWeaponDamage * fWeaponCritical;
                LizardCanvas.GetComponent<LizardHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.LizardHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fLizardHp -= fSphereWeaponDamage;
            LizardCanvas.GetComponent<LizardHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.LizardHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fLizardHp -= fBatPetWeaponDamage;
            LizardCanvas.GetComponent<LizardHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }




  






}
