using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public static Plant Instance
    {
        get
        {
            if (PlantInstance == null)
            {
                PlantInstance = FindObjectOfType<Plant>();
                if (PlantInstance == null)
                {
                    var InstanceContainer = new GameObject("Plant");
                    PlantInstance = InstanceContainer.AddComponent<Plant>();
                }
            }
            return PlantInstance;
        }
    }

    private static Plant PlantInstance;

    RoomChecker roomIsChecker;

    public GameObject Player;
    //Treeshooter재활용 할것.
    public GameObject DangerLine;
    public GameObject PlantShooter;
    public GameObject PlantCanvas;

     Animator anim;


    public Transform ShooterPosition;

    public LayerMask layerMask;

    public float fPlantHp = 1490f;
    public float fPlantMaxHp = 1490f;


    public float fWeaponDamage;
    public float fBowWeaponDamage;
    public float fSphereWeaponDamage;
    public float fWeaponCritical = 1.3f; 
    public float fBatPetWeaponDamage;
    public float fCollisionDelay = 2f;

    public bool bCollisionDelay = true;

    Vector3 RenwealPosition;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        roomIsChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();



        anim = GetComponent<Animator>();

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Idle");
        }
        StartCoroutine(StartAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (fPlantHp <= 0)
        {

            fPlantHp = 0f;

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



    IEnumerator StartAttack()
    {
        yield return null;

        while (!roomIsChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.7f);
        }
        yield return new WaitForSeconds(1f);
        while(true)
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

                //공격모션에 두번을 공격한다. 
                anim.SetTrigger("Attack");
                yield return new WaitForSeconds(1.8f);


    


            yield return null;

    
            }


        }



    }



    public void Attack()
    {
        SoundManager.Instance.MonsterSound("PlantShooterSound", SoundManager.Instance.bgList[30]);
        Instantiate(PlantShooter, ShooterPosition.transform.position, transform.rotation);
        Invoke("LateAttack", 0.1f);

    }

    public void LateAttack()
    {
        SoundManager.Instance.MonsterSound("PlantShooterSound", SoundManager.Instance.bgList[30]);
        Instantiate(PlantShooter, ShooterPosition.transform.position, transform.rotation);
        Invoke("LastAttack", 0.1f);
    }

    public void LastAttack()
    {
        SoundManager.Instance.MonsterSound("PlantShooterSound", SoundManager.Instance.bgList[30]);
        Instantiate(PlantShooter, ShooterPosition.transform.position, transform.rotation);

    }


    void TrailMaker()
    {
        RenwealPosition = new Vector3(transform.position.x, transform.position.y + 0.3f , transform.position.z );

        Physics.Raycast(RenwealPosition, transform.forward, out RaycastHit hit, 30f, layerMask);
        if(hit.transform != null && hit.transform.CompareTag("Wall"))
        {
            GameObject DanagerLineCopy  = Instantiate(DangerLine, RenwealPosition, transform.rotation);

            DanagerLineCopy.GetComponent<PlantDangerLine>().EndPosition = hit.point;
          
        }
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
                fPlantHp -= fWeaponDamage;
                PlantCanvas.GetComponent<PlantHpbar>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);

            }
            else
            {
                fPlantHp -= fWeaponDamage * fWeaponCritical;
                PlantCanvas.GetComponent<PlantHpbar>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, true);

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
                fPlantHp -= fBowWeaponDamage;
                PlantCanvas.GetComponent<PlantHpbar>().Damage(fBowWeaponDamage);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage, false);
            }
            else
            {
                fPlantHp -= fBowWeaponDamage * fWeaponCritical;
                PlantCanvas.GetComponent<PlantHpbar>().Damage(fBowWeaponDamage * fWeaponCritical);
                BowDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBowWeaponDamage * fWeaponCritical, true);
            }
            Instantiate(EffectScript.Instance.SnakeHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("SphereWeapon"))
        {
            fSphereWeaponDamage = other.gameObject.GetComponent<PetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fPlantHp -= fSphereWeaponDamage;
            PlantCanvas.GetComponent<PlantHpbar>().Damage(fSphereWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);
            Instantiate(EffectScript.Instance.SnakeHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("BatPetWeapon"))
        {
            fBatPetWeaponDamage = other.gameObject.GetComponent<BatPetWeapon>().fDamage;
            GameObject SphereDamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);

            fPlantHp -= fBatPetWeaponDamage;
            PlantCanvas.GetComponent<PlantHpbar>().Damage(fBatPetWeaponDamage);
            SphereDamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fBatPetWeaponDamage, false);
            Instantiate(EffectScript.Instance.ArcheroHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(other.gameObject);
        }

    }






}
