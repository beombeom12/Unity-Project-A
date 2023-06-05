using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class PlayerMove : MonoBehaviour
{

    public static PlayerMove Instance 
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerMove>();
                if(instance == null)
                {
                    var InstanceContainer = new GameObject("PlayerMove");
                    instance = InstanceContainer.AddComponent<PlayerMove>();
                }
            }
            return instance;
        }
    }
    private static PlayerMove instance;
    



    //PlayerMove
    public Rigidbody PlayerRigidBody;
    public Animator anim;
    private Vector3 Movement;


    public bool isMove = false;
    public bool bIsPause = false;

    public GameObject GoRoulette;
    public GameObject GoRouletteBg;
    public GameObject GoRoulettePenal;
    public GameObject JoyStickPanel;
    public GameObject JoyStickBg;


    public bool bCollisionDelay = true;
    public float fCollisionDelay = 10f;
    public float fSpeed = 3f;

    public float WheelDamage;
    public float ThroneDamage;



    //for testing
    float timer;
    float waitingTime;


    private void Start()
    {
        PlayerRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        timer = 0.0f;
        waitingTime = 0.28f;




        
    }

    void Update()
    {
       



        timer += Time.deltaTime;

        if(timer > waitingTime)
        {
            if (isMove && JoyStickScript.Instance.joyVec.x != 0 || JoyStickScript.Instance.joyVec.y != 0)
            {
                Instantiate(EffectScript.Instance.PlayerWalkEffect, transform.position, transform.rotation);
                SoundManager.Instance.PlayerSound("PlayerWalkSound", SoundManager.Instance.bgList[17]);
                SoundManager.Instance.PlayerSound("PlayerWalkSound", SoundManager.Instance.bgList[16]);

                timer = 0f;
            }
        }
        else if(!isMove && JoyStickScript.Instance.joyVec.x == 0 || JoyStickScript.Instance.joyVec.y == 0)
        {
            timer = 0f;
       
        }
        


      
    }

    private void FixedUpdate()
    {
        if (isMove && JoyStickScript.Instance.joyVec.x != 0 || JoyStickScript.Instance.joyVec.y != 0)
        {
            PlayerRigidBody.velocity = new Vector3(JoyStickScript.Instance.joyVec.x, 0, JoyStickScript.Instance.joyVec.y) * fSpeed;
            PlayerRigidBody.rotation = Quaternion.LookRotation(new Vector3(JoyStickScript.Instance.joyVec.x, 0, JoyStickScript.Instance.joyVec.y));



        }

    }


    


    private void OnTriggerEnter(Collider other)
    {
        //다음방으로 넘어갈 트리거
        if (other.transform.CompareTag("NextRoom"))
        {
            Debug.Log("Get Next Room");
            SceneMgr.Instance.NextStage();

        }

        ////경험치
        if (PlayerTargeting.Instance.MonsterList.Count <= 0 && other.transform.CompareTag("Exp"))
        {
            PlayerSkillData.Instance.PlayerExp(45f);
            MenuData.Instance.PlayerGetShooter(10f);
            Destroy(other.gameObject.transform.parent.gameObject, 0.0001f);
        }

        //HelathPack
        if (PlayerTargeting.Instance.MonsterList.Count <= 0 && other.transform.CompareTag("Item"))
        {
            PlayerSkillData.Instance.ItemHealthpack();
            Destroy(other.gameObject, 0.001f);
        }

        //천사방!
        if (other.transform.CompareTag("Angel"))
        {



            SoundManager.Instance.PlayerSound("AngelTriggerSound", SoundManager.Instance.bgList[4]);


            GoRouletteBg.SetActive(true);
            GoRoulettePenal.SetActive(true);
            StartCoroutine(RoulettGo());
            GoRoulette.SetActive(true);
            JoyStickPanel.SetActive(false);
            JoyStickBg.SetActive(false);

            //RouletteMgr.Instance.RouletteRight.SetActive(true);
            //RouletteMgr.Instance.RoulettePenal.SetActive(true);
            //RouletteMgr.Instance.RouletteBackGround.SetActive(true);


            //RouletteMgr.Instance.JoyStickPanel.SetActive(false);
            //RouletteMgr.Instance.JoyStickBg.SetActive(false);



        }


        //몬스터

        //뱀 고스트 
        if(other.transform.CompareTag("MeleeAttack"))
        {
            PlayerSkillData.Instance.PlayerTakeDamage(80f);
            PlayerHpBar.Instance.TakeDamaged(80f);
            Instantiate(EffectScript.Instance.FireHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Instantiate(EffectScript.Instance.PlayerHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }


        //해골전사
        if (other.transform.CompareTag("WarriorSkeleton"))
        {
            PlayerHpBar.Instance.TakeDamaged(200f);
            PlayerSkillData.Instance.PlayerTakeDamage(200f);
            Instantiate(EffectScript.Instance.FireHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Instantiate(EffectScript.Instance.PlayerHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        //Lizard
        if (other.transform.CompareTag("FireWeapon"))
        {
            PlayerHpBar.Instance.TakeDamaged(300f);
            PlayerSkillData.Instance.PlayerTakeDamage(300f);
            Instantiate(EffectScript.Instance.FireHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        //분해되는 중간 해골 몬스터 
        if(other.transform.CompareTag("MiddleMeleeAttack"))
        {
            PlayerHpBar.Instance.TakeDamaged(250f);
            PlayerSkillData.Instance.PlayerTakeDamage(250f);
            Instantiate(EffectScript.Instance.FireHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }




        
        if (other.transform.CompareTag("BossMeleeAttack"))
        {
            //PlayerHpBar.Instance.TakeDamaged(300f);
            //PlayerSkillData.Instance.fPlayerCurrentHp -= other.transform.GetComponent<LastBoss>().MonsterDamage;
        }






        //장애물
        //가시
        if (other.transform.CompareTag("Throne"))
        {
            StartCoroutine(SetUpThroneDelay());
        }

        ////경험치
        //if (PlayerTargeting.Instance.MonsterList.Count <= 0 && collision.transform.CompareTag("Exp"))
        //{
        //    PlayerSkillData.Instance.PlayerExp(45f);
        //    MenuData.Instance.PlayerGetShooter(10f);
        //    Destroy(other.gameObject.transform.parent.gameObject, 0.0001f);
        //}


    }




    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.CompareTag("RangeAtk"))
        {
            Destroy(collision.gameObject, 0.1f);

            PlayerHpBar.Instance.TakeDamaged(300f);
            PlayerSkillData.Instance.fPlayerCurrentHp -= collision.transform.GetComponent<LastBossBolt>().fDamage;
        }

        if(collision.transform.CompareTag("Wheel"))
        {
            StartCoroutine(RotateCollisionWheelDelay());

        }

        //if (PlayerTargeting.Instance.MonsterList.Count <= 0 && collision.transform.CompareTag("Exp"))
        //{
        //    PlayerSkillData.Instance.PlayerExp(45f);
        //    MenuData.Instance.PlayerGetShooter(10f);
        //    Destroy(collision.gameObject.transform.parent.gameObject, 0.0001f);
        //}





    }



        
   





    IEnumerator RotateCollisionWheelDelay()
    {
        bCollisionDelay = false;

        WheelDamage = Wheel.Instance.fDamage;
        PlayerHpBar.Instance.TakeDamaged(WheelDamage);
        PlayerSkillData.Instance.PlayerTakeDamage(WheelDamage);
        yield return new WaitForSeconds(3f);
        bCollisionDelay = true;
    }


    IEnumerator SetUpThroneDelay()
    {
        //bCollisionDelay = false;
        ThroneDamage = ObstacleManager.Instance.fThroneDamage;
        PlayerHpBar.Instance.TakeDamaged(ThroneDamage);
        PlayerSkillData.Instance.PlayerTakeDamage(ThroneDamage);
        Instantiate(EffectScript.Instance.FireHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Instantiate(EffectScript.Instance.PlayerHittedEffect, transform.position, Quaternion.Euler(0f, 90f, 0f));
        SoundManager.Instance.PlayerSound("ThroneHittedSound", SoundManager.Instance.bgList[3]);
        bCollisionDelay = true;
        yield return new WaitForSeconds(1f);

    }


    IEnumerator RoulettGo()
    {
        yield return new WaitForSeconds(2f);

        float fSpeed = Random.Range(1f, 5f);
        float fRotateSpeed = 100f * fSpeed;



        while (true)
        {
            //SoundManager.Instance.GameUISound("GameUISound", SoundManager.Instance.bgList[24]);
            yield return null;
            //if (fRotateSpeed <= 500f)
            //{
            //    SoundManager.Instance.GameUISoundStop("GameUiSound", SoundManager.Instance.bgList[24]);
            //}
            
            
            
            if (fRotateSpeed <= 0.01f)
            {
              
                break;


            }
            fRotateSpeed = Mathf.Lerp(fRotateSpeed, 0, Time.deltaTime * 2f);
            GoRouletteBg.transform.Rotate(0, 0, fRotateSpeed);



        }

        yield return new WaitForSeconds(1.5f);

        RouletteMgr.Instance.SetResult();

        yield return new WaitForSeconds(1f);

        GoRoulette.SetActive(false);
        GoRouletteBg.SetActive(false);
        GoRoulettePenal.SetActive(false);



        JoyStickPanel.SetActive(true);
        JoyStickBg.SetActive(true);


    }

   
}