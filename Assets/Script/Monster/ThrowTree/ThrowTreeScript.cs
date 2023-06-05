using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTreeScript : MonoBehaviour
{
    public static ThrowTreeScript Instance
    {
        get
        {
            if (ThrowTreeInstance == null)
            {
                ThrowTreeInstance = FindObjectOfType<ThrowTreeScript>();
                if (ThrowTreeInstance == null)
                {
                    var InstanceContainer = new GameObject("ThrowTreeScript");
                    ThrowTreeInstance = InstanceContainer.AddComponent<ThrowTreeScript>();
                }
            }
            return ThrowTreeInstance;
        }
    }

    private static ThrowTreeScript ThrowTreeInstance;

    RoomChecker roomIsChecker;

    public GameObject Player;
    public GameObject DangerLine;
    public GameObject TreeShooter;
    public GameObject ThrowTreeCanvas;

    public Transform ShooterPosition;

    public LayerMask layerMask;

    public float fThrowTreeHp = 1290f;
    public float fThrowTreeMaxHp = 1290f;

    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fWeaponCritical = 1.3f;
    public float fCollisionDelay = 2f;
    public float fBatPetWeaponDamage;
    public bool bCollisionDelay = true;


     Animator anim;

    Vector3 RenewalPosition;

    private  void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");


        anim = GetComponent<Animator>();

        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Idle");
        }
        StartCoroutine(ReadyforPlayer());

    }

    private void Update()
    {

        if (fThrowTreeHp <= 0)
        {
            fThrowTreeHp = 0;
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


    }



    IEnumerator ReadyforPlayer()
    {
        yield return null;

        while (!roomIsChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);
        while (true)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.SetTrigger("Idle");
            }
            yield return new WaitForSeconds(0.5f);
            transform.LookAt(Player.transform.position);
            yield return new WaitForSeconds(1.3f);
            TrailMaker();
            yield return new WaitForSeconds(1.3f);
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(1.8f);
                SoundManager.Instance.MonsterSound("ThrowTreeShoot", SoundManager.Instance.bgList[32]);
                GameObject DangerLineCopy = Instantiate(TreeShooter, RenewalPosition, transform.rotation);
            }

        }




    }


    void TrailMaker()
    {
        RenewalPosition =  new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Physics.Raycast(RenewalPosition, transform.forward, out RaycastHit hit, 30f, layerMask);

        if (hit.transform != null && hit.transform.CompareTag("Wall"))
        {
            GameObject DangerLineCopy = Instantiate(DangerLine, RenewalPosition, transform.rotation);

            DangerLineCopy.GetComponent<TreeDangerLine>().EndPosition = hit.point;

        }


    }





    void EveneShooter()
    {
        SoundManager.Instance.MonsterSound("ThrowTreeShoot", SoundManager.Instance.bgList[32]);
        GameObject DangerLineCopy = Instantiate(TreeShooter, RenewalPosition, transform.rotation);
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));

            GameObject DamageTextClone = Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            if (Random.value < 0.6)
            {
                fThrowTreeHp -= fWeaponDamage;
                ThrowTreeCanvas.GetComponent<ThrowTreeHpBar>().Damage(fWeaponDamage);

                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {
                fThrowTreeHp -= fWeaponDamage * fWeaponCritical;
                ThrowTreeCanvas.GetComponent<ThrowTreeHpBar>().Damage(fWeaponDamage * fWeaponCritical);

                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, true);
            }
            Instantiate(EffectScript.Instance.ThrowTreeHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
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
                fThrowTreeHp -= fBowWeaponDamage;
                ThrowTreeCanvas.GetComponent<ThrowTreeHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fThrowTreeHp -= fBowWeaponDamage * fWeaponCritical;
                ThrowTreeCanvas.GetComponent<ThrowTreeHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);

            }
            Instantiate(EffectScript.Instance.ThrowTreeHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fThrowTreeHp -= fSphereWeaponDamage;
            ThrowTreeCanvas.GetComponent<ThrowTreeHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.ThrowTreeHitted, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }



        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fThrowTreeHp -= fBatPetWeaponDamage;
            ThrowTreeCanvas.GetComponent<ThrowTreeHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }



    }



   

}