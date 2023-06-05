using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheroMonster : MonoBehaviour
{
    public static ArcheroMonster Instance
    {
        get
        {
            if (ArcheroInstance == null)
            {
                ArcheroInstance = FindObjectOfType<ArcheroMonster>();
                if (ArcheroInstance == null)
                {
                    var InstanceContainer = new GameObject("ArcheroMonster");
                    ArcheroInstance = InstanceContainer.AddComponent<ArcheroMonster>();
                }
            }
            return ArcheroInstance;
        }
    }
    private static ArcheroMonster ArcheroInstance;


    RoomChecker roomIsChecker;

    Animator Anim;


    public GameObject Player;

    public GameObject Shot;
    public GameObject ArcheroCanvas;
    public GameObject DangerLine;
    public Transform ShooterPosition;

    public LayerMask layerMask;

    public float fArcheroHp = 1190f;
    public float fArcheroMaxHp = 1190f;
    //이게 표창 데미지
    public float fWeaponDamage;
    //활 데미지
    public float fBowWeaponDamage;
    //이게 펫
    public float fSphereWeaponDamage;

    public float fBatPetWeaponDamage;
    
    public float fWeponCritical = 1.3f;
    public float fCollisionDelay = 2f;

    public bool bCollisionDelay = true;
    public bool blookAtPlayer = true;

    Vector3 RenwealPosition;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");




        Anim = GetComponent<Animator>();

        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();

        

        StartCoroutine(ReadyForAttack());
        


    }




    // Update is called once per frame
    void Update()
    {
        if(fArcheroHp <= 0)
        {
            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;
            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.iTargetingIndex = -1;
            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.iTargetingIndex = -1;

            Instantiate(EffectScript.Instance.MonsterDeadEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));


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


    IEnumerator ReadyForAttack()
    {

        yield return null;

        while (!roomIsChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);

        while (true)
        {
            
            transform.LookAt(Player.transform.position);
            yield return new WaitForSeconds(0.5f);
            TrailMaker();
            yield return new WaitForSeconds(2.5f);
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Anim.SetTrigger("Attack");


            }
            yield return new WaitForSeconds(1.1f);
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Anim.SetTrigger("Idle");
            }
        }
    }


        void TrailMaker()
        {
            RenwealPosition = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
             Physics.Raycast(RenwealPosition, transform.forward, out RaycastHit hit, 30f, layerMask);
            if (hit.transform != null && hit.transform.CompareTag("Wall"))
            {
                GameObject DanagerLineCopy = Instantiate(DangerLine, RenwealPosition, transform.rotation);

                DanagerLineCopy.GetComponent<ArcheroDangerLine>().EndPoisition = hit.point;

            }
        }



    public void EventShot()
    {
        SoundManager.Instance.MonsterSound("ArcheroMonsterAttacking", SoundManager.Instance.bgList[12]);
        Instantiate(Shot, ShooterPosition.transform.position, transform.rotation);
    }




    private void OnTriggerEnter(Collider other)
    {
        //이게 표창
        if (other.transform.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;
            GameObject DamageTextClone =
            Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
            if (Random.value < 0.6)
            {
                fArcheroHp -= fWeaponDamage;
                ArcheroCanvas.GetComponent<ArcheroHpBar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);

            }
            else
            {
                fArcheroHp -= fWeaponDamage * fWeponCritical;
                ArcheroCanvas.GetComponent<ArcheroHpBar>().Damage(fWeaponDamage * fWeponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage * fWeponCritical, true);
            }
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("BowWeapon"))
        {
            fBowWeaponDamage = other.gameObject.GetComponent<PlayerBowShooter>().fDamage;
            Instantiate(EffectScript.Instance.BowHittingEffect, transform.position, Quaternion.identity);
            GameObject BowDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);


           if(Random.value < 0.6)
            {
                fArcheroHp -= fBowWeaponDamage;
                ArcheroCanvas.GetComponent<ArcheroHpBar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
           else
            {
                fArcheroHp -= fBowWeaponDamage * fWeponCritical;
                ArcheroCanvas.GetComponent<ArcheroHpBar>().Damage(fBowWeaponDamage * fWeponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeponCritical, true);

            }
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fArcheroHp -= fSphereWeaponDamage;
            ArcheroCanvas.GetComponent<ArcheroHpBar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }


        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fArcheroHp -= fBatPetWeaponDamage;
            ArcheroCanvas.GetComponent<ArcheroHpBar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }
    }







}
