using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMagicianSkeleton : MonoBehaviour
{
    public static SmallMagicianSkeleton Instance
    {
        get
        {
            if (SmallMagicianSkeletonInstance == null)
            {
                SmallMagicianSkeletonInstance = FindObjectOfType<SmallMagicianSkeleton>();
                if (SmallMagicianSkeletonInstance == null)
                {
                    var InstanceContainer = new GameObject("MiniDragon");
                    SmallMagicianSkeletonInstance = InstanceContainer.AddComponent<SmallMagicianSkeleton>();
                }
            }
            return SmallMagicianSkeletonInstance;
        }
    }
    private static SmallMagicianSkeleton SmallMagicianSkeletonInstance;
    RoomChecker roomIsChecker;

    Animator Anim;

    public GameObject Player;
    public GameObject MiniDrageCanvase;
    public GameObject MeleeAttack;
    public GameObject RealFireShoot;

    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fWeaponCritical = 1.3f;
    public float fBatPetWeaponDamage;

    public float fSmallMagicianHp = 1450f;
    public float fSmallMagicianMaxHp = 1450f;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Anim = GetComponent<Animator>();
        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();

        StartCoroutine(ReadyForPlayer());
    }

    void Update()
    {

        if (fSmallMagicianHp <= 0)
        {
            fSmallMagicianHp = 0;


            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));

            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;
            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.iTargetingIndex = -1;

            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.iTargetingIndex = -1;


            Instantiate(EffectScript.Instance.SkeletonDeadEffect, transform.position, transform.rotation);
            Vector3 vCurrentPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
     
             GameObject ExpClone = Instantiate(PlayerSkillData.Instance.CoinExp, vCurrentPosition, transform.rotation);
             SoundManager.Instance.MonsterSound("MonsterDropSound", SoundManager.Instance.bgList[11]);
             ExpClone.transform.parent = gameObject.transform.parent.parent;
            


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

        while (!roomIsChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.8f);
        }
        yield return new WaitForSeconds(1f);

        while (true)
        {
            transform.LookAt(Player.transform.position);

            yield return new WaitForSeconds(1.5f);
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Anim.SetTrigger("Attack");
                yield return null;
            }
            yield return new WaitForSeconds(1.2f);
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Anim.SetTrigger("Idle");
            }

        }
    }


    public void EvenetShot()
    {

        SoundManager.Instance.MonsterSound("Smallmagician", SoundManager.Instance.bgList[43]);
        Instantiate(RealFireShoot, MeleeAttack.transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(-90f, 0f, 0f)));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;
            GameObject DamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.Euler(90f, 0f, 0f));
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            if (Random.value < 0.6)
            {
                fSmallMagicianHp -= fWeaponDamage;
                MiniDrageCanvase.GetComponent<SmallMagicianSkeletonHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {
                fSmallMagicianHp -= fWeaponDamage * fWeaponCritical;
                MiniDrageCanvase.GetComponent<SmallMagicianSkeletonHpBar>().Damage(fWeaponDamage * fWeaponCritical);
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
                fSmallMagicianHp -= fBowWeaponDamage;
                MiniDrageCanvase.GetComponent<SmallMagicianSkeletonHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fSmallMagicianHp -= fBowWeaponDamage * fWeaponCritical;
                MiniDrageCanvase.GetComponent<SmallMagicianSkeletonHpBar>().Damage(fBowWeaponDamage * fWeaponCritical);
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

            fSmallMagicianHp -= fSphereWeaponDamage;
            MiniDrageCanvase.GetComponent<SmallMagicianSkeletonHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.BombGhostHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fSmallMagicianHp -= fBatPetWeaponDamage;
            MiniDrageCanvase.GetComponent<SmallMagicianSkeletonHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

    }

}
