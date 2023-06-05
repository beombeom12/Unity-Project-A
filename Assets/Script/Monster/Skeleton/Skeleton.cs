using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public static Skeleton Instance
    {
        get
        {
            if (SkeletonInstance == null)
            {
                SkeletonInstance = FindObjectOfType<Skeleton>();
                if (SkeletonInstance == null)
                {
                    var InstanceContainer = new GameObject("Skeleton");
                    SkeletonInstance = InstanceContainer.AddComponent<Skeleton>();
                }
            }
            return SkeletonInstance;
        }
    }
    private static Skeleton SkeletonInstance;




    public GameObject Player;
    public GameObject SkeletonCanavas;

    Animator Anim;

    public float fSkeletonHp = 8500f;
    public float fSkeletonMaxHp = 8500f;
    public float fWeaponCritical = 1.3f;
    public float fBatPetWeaponDamage;
    public float fCollisionDelay = 2f;
    float fWeaponDamage;
    float fBowWeaponDamage;
    float fSphereWeaponDamage;


    public bool bCollisionDelay = true;
    public bool bHasExit;
    public bool bHasExit2;

    public RoomChecker roomIsChecker;
    //public GameObject Close;
    private const float MIN_SPLIT_HEALTH = 0f;


    private const float MIN_SPLIT2_HELATH = 3000f;


    public float speed = 20f;


    public GameObject splitMonsterPrefab;
    GameObject splitMonster1;
    GameObject splitMonster2;


    void Start()
    {
    




        Player = GameObject.FindGameObjectWithTag("Player");

        Anim = GetComponent<Animator>();

        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();


        transform.rotation = Quaternion.Euler(new Vector3(0f, -135f, 0f));



    }

  
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (fSkeletonHp <= MIN_SPLIT_HEALTH)
        {
    
            if (bHasExit == false)
            {
                Split();
                Die();
                bHasExit = true;
             
            }

            fSkeletonHp = 0f;

            Die();

            Destroy(transform.gameObject);
        }

     

    }





    private void Split()
    {


        fSkeletonHp = fSkeletonMaxHp / 2f;


        Vector3 scale = transform.localScale * 0.5f;
        splitMonster1 = Instantiate(splitMonsterPrefab,new Vector3( transform.position.x + 0.1f, transform.position.y, transform.position.z + 0.1f )+ Vector3.right , Quaternion.identity, roomIsChecker.transform.Find("MiddleBoss"));
        splitMonster2 = Instantiate(splitMonsterPrefab, transform.position + Vector3.left, Quaternion.identity, roomIsChecker.transform.Find("MiddleBoss"));
        splitMonster1.transform.localScale = scale;
        splitMonster1.GetComponent<Skeleton>().fSkeletonMaxHp = fSkeletonMaxHp / 2f;
        splitMonster1.GetComponent<Skeleton>().fSkeletonHp = fSkeletonMaxHp / 2f;
        splitMonster1.GetComponent<Skeleton>().roomIsChecker = roomIsChecker;
  //      splitMonster1.GetComponent<SkeletonHpBar>().fSkeletonHp = SkeletonHpBar.Instance.fSkeletonHp / 2f;
//        splitMonster1.GetComponent<SkeletonHpBar>().fSkeletonMaxHp = SkeletonHpBar.Instance.fSkeletonMaxHp / 2f;

        splitMonster2.transform.localScale = scale;
        splitMonster2.GetComponent<Skeleton>().fSkeletonMaxHp = fSkeletonMaxHp / 2f;
        splitMonster2.GetComponent<Skeleton>().fSkeletonHp = fSkeletonMaxHp / 2f;
        splitMonster2.GetComponent<Skeleton>().roomIsChecker = roomIsChecker;
        //splitMonster2.GetComponent<SkeletonHpBar>().fSkeletonHp = SkeletonHpBar.Instance.fSkeletonHp / 2f;
        //splitMonster2.GetComponent<SkeletonHpBar>().fSkeletonMaxHp = SkeletonHpBar.Instance.fSkeletonMaxHp / 2f;

        splitMonster1.GetComponent<Skeleton>().bHasExit = true;
        splitMonster2.GetComponent<Skeleton>().bHasExit = true;


        Destroy(transform.gameObject);
        PlayerTargeting.Instance.MonsterList.Add(splitMonster1);
        PlayerTargeting.Instance.MonsterList.Add(splitMonster2);




    }


    

    private void Die()
    {
        Instantiate(EffectScript.Instance.SkeletonDeadEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
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
                fSkeletonHp -= fWeaponDamage;
               SkeletonCanavas.GetComponent<SkeletonHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);

            }
            else
            {
                fSkeletonHp -= fWeaponDamage * fWeaponCritical;
                SkeletonCanavas.GetComponent<SkeletonHpBar>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);
            }
            Instantiate(EffectScript.Instance.RotateSkeletonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                fSkeletonHp -= fBowWeaponDamage;
                SkeletonCanavas.GetComponent<SkeletonHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fSkeletonHp -= fBowWeaponDamage * fWeaponCritical;
                SkeletonCanavas.GetComponent<SkeletonHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.RotateSkeletonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fSkeletonHp -= fSphereWeaponDamage;
            SkeletonCanavas.GetComponent<SkeletonHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.RotateSkeletonHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fSkeletonHp -= fBatPetWeaponDamage;
            SkeletonCanavas.GetComponent<SkeletonHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 벽과 충돌 시 각도 변경
            transform.Rotate(Vector3.up, -55f);
        }
    }

}
