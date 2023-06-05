using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpShotMonster : MonoBehaviour
{
    public static UpShotMonster Instance
    {
        get
        {
            if (UpShotInstance == null)
            {
                UpShotInstance = FindObjectOfType<UpShotMonster>();
                if (UpShotInstance == null)
                {
                    var InstanceContainer = new GameObject("UpShotMonster");
                    UpShotInstance = InstanceContainer.AddComponent<UpShotMonster>();
                }
            }
            return UpShotInstance;
        }
    }
    private static UpShotMonster UpShotInstance;



    RoomChecker roomIsChecker;
    Animator anim;
    Rigidbody UpShotMosnterRigid;


    public GameObject Player;
    public GameObject UpShotCanvas;
    public GameObject BulletShoot;
    public GameObject BulletShoot1;
    public GameObject BulletShoot2;


    public Transform ShooterPosition;

    public float fUpShotHp = 1450f;
    public float fUpShotMaxHp = 1450f;
    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fWeaponCritical = 1.3f;
    public float fCollisionDelay = 2f;

    public bool bCollisionDelay = true;






    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();

        UpShotMosnterRigid = GetComponent<Rigidbody>();

        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();


        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Idle");
        }
        StartCoroutine(ReadyForPlayer());
    }

    // Update is called once per frame
    void Update()
    {

        if(fUpShotHp <= 0)
        {
            fUpShotHp = 0f;
            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));
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


        UpShotMosnterRigid.velocity = Vector3.zero;
     
    
    }


    IEnumerator ReadyForPlayer()
    {
        yield return null;

        while(!roomIsChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(1f);
        }
        while(true)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.SetTrigger("Idle");
            }
            yield return new WaitForSeconds(3.5f);


            ResetAttackPosition();

           
            yield return new WaitForSeconds(2.5f);
            

            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.SetTrigger("Attack");

                SoundManager.Instance.MonsterSound("UpShotMonster", SoundManager.Instance.bgList[41]);
                Instantiate(BulletShoot, ShooterPosition.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Instantiate(BulletShoot1, ShooterPosition.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Instantiate(BulletShoot2, ShooterPosition.position, Quaternion.identity);


            }


        }

    }

    public void ResetAttackPosition()
    {
        transform.LookAt(Player.transform.position);

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
                fUpShotHp -= fWeaponDamage;
                UpShotCanvas.GetComponent<UpShotHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {
                fUpShotHp -= fWeaponDamage * fWeaponCritical;
                UpShotCanvas.GetComponent<UpShotHpBar>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);
            }
            Instantiate(EffectScript.Instance.UpShotMonsterHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                fUpShotHp -= fBowWeaponDamage;
                UpShotCanvas.GetComponent<UpShotHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fUpShotHp -= fBowWeaponDamage * fWeaponCritical;
                UpShotCanvas.GetComponent<UpShotHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.UpShotMonsterHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fUpShotHp -= fSphereWeaponDamage;
            UpShotCanvas.GetComponent<UpShotHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.UpShotMonsterHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


    }


   
}

