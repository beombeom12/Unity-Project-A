using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGolem : MonoBehaviour
{

    public static SmallGolem Instance
    {
        get
        {
            if (SmallGolemInstance == null)
            {
                SmallGolemInstance = FindObjectOfType<SmallGolem>();
                if (SmallGolemInstance == null)
                {
                    var InstanceContainer = new GameObject("SmallGolem");
                    SmallGolemInstance = InstanceContainer.AddComponent<SmallGolem>();
                }
            }
            return SmallGolemInstance;
        }
    }

    RoomChecker roomIsChecker;

    Animator Anim;

    public GameObject Player;
    public GameObject Shot;
    public GameObject SmallGolemCanvas;

    public Transform ShooterPosition;

    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fCollisionDelay  = 2f;
    public float fSmallGolemHp = 1690f;
    public float fSmallgolemMaxHp = 1690f;
    private float fWeaponCritical = 1.3f;
    public float fBatPetWeaponDamage;
    public bool bCollisionDelay = true;






    private static SmallGolem SmallGolemInstance;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Anim = GetComponent<Animator>();
        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();

        StartCoroutine(ReadyForPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(fSmallGolemHp <= 0)
        {
            fSmallGolemHp = 0;


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
            return;
        }
    }



    IEnumerator ReadyForPlayer()
    {
        yield return null;

        while(!roomIsChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.8f);
        }
        yield return new WaitForSeconds(1f);

        while(true)
        {
            transform.LookAt(Player.transform.position);

            yield return new WaitForSeconds(1.5f);
            if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Anim.SetTrigger("Attack");
                yield return null;
            }
            yield return new WaitForSeconds(1.2f);
            if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Anim.SetTrigger("Idle");
            }

        }
    }



    public void EvenetShot()
    {
        SoundManager.Instance.MonsterSound("SmallGolemSound", SoundManager.Instance.bgList[39]);
        Instantiate(Shot, ShooterPosition.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, -25f, 0f)));

        Instantiate(Shot, ShooterPosition.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0f, 0f)));

        Instantiate(Shot, ShooterPosition.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 25f, 0f)));

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
                fSmallGolemHp -= fWeaponDamage;
                SmallGolemCanvas.GetComponent<SmallGolemHpbar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);

            }
            else
            {
                fSmallGolemHp -= fWeaponDamage * fWeaponCritical;
                SmallGolemCanvas.GetComponent<SmallGolemHpbar>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeaponCritical, true);
            }
            Instantiate(EffectScript.Instance.SmallGolemHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                fSmallGolemHp -= fBowWeaponDamage;
                SmallGolemCanvas.GetComponent<SmallGolemHpbar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fSmallGolemHp -= fBowWeaponDamage * fWeaponCritical;
                SmallGolemCanvas.GetComponent<SmallGolemHpbar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.SmallGolemHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fSmallGolemHp -= fSphereWeaponDamage;
            SmallGolemCanvas.GetComponent<SmallGolemHpbar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.SmallGolemHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fSmallGolemHp -= fBatPetWeaponDamage;
            SmallGolemCanvas.GetComponent<SmallGolemHpbar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

    }







}
