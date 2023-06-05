using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance
    {
        get
        {
            if (TargetInstance == null)
            {
                TargetInstance = FindObjectOfType<PlayerTargeting>();
                if (TargetInstance == null)
                {
                    var InstanceContainer = new GameObject("PlayerTargeting");
                    TargetInstance = InstanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return TargetInstance;
        }
    }
    private static PlayerTargeting TargetInstance;


    //몬스터를 우선 담을수있는 리스트 설정
    public List<GameObject> MonsterList = new List<GameObject>();

    //나중에 설정될 무기
    public GameObject PlayerShooter;

    //활
    public GameObject PlayerBowShooter;


    //UI활과 표창

    public GameObject UICommandWeapon;
    public GameObject UIBowWeapon;


    public Transform ShooterPoint;
    public Transform BowShootPoint;


    public Transform MonsterTargeting;
    public GameObject MonsterTargetings;
    public float fRate;

    public bool bIsTargetIng = false;

    float fCurrentDistance = 0;
    float fNearDistance = 100f;
    float fTargetingDistance = 100f;

    public float fAttackingSpeed = 2f;
    //가까운 인덱스
    public int icloseDistanceIndex = 0;
    public int iTargetingIndex = -1;



    //레이어를 설정해서 자기자신을 공격하거나 벽등을 공격하지 않기 위해서
    public LayerMask layerMask;



    public float fDelayTime = 3f;


    private void OnDrawGizmos()
    {
        //타켓팅중이고 가상의 레이저를 생성
        if (bIsTargetIng && MonsterList != null && MonsterList.Count != 0)
        {
            for (int i = 0; i < MonsterList.Count; i++)
            {
                if (MonsterList[i] == null) continue;
                RaycastHit hit;
                bool bHiting = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out hit, 100f, layerMask);

                if (bHiting && hit.transform.CompareTag("Monster"))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawRay(transform.position, MonsterList[i].transform.position - transform.position);
            }




        }


    }


    private void Start()
    {
        fAttackingSpeed = fAttackingSpeed + ButtonController.Instance.fAttackingSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        Targeting();
        AnimationAttackTarget();


        if (MonsterList.Count == 0)
        {
            MonsterTargetings.SetActive(false);
        }

    }

 
  



    public void BowAttack()
    {
        PlayerMove.Instance.anim.SetFloat("AttackSpeed", fAttackingSpeed);
        Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[2]], new Vector3(ShooterPoint.transform.position.x, ShooterPoint.transform.position.y, ShooterPoint.transform.position.z), transform.rotation);
        SoundManager.Instance.PlayerSound("PlayerBowSound", SoundManager.Instance.bgList[12]);
    //전방으로 두발씩 쏘는것[1]
        if(PlayerSkillData.Instance.BowPlayerSkill[1] > 0)
        {
            Invoke("MultiBow", 0.2f);
        }

        //사선방향으로 나아가는 화살[3]
        //0 일경우 45 도 두개
        if(PlayerSkillData.Instance.BowPlayerSkill[3] > 0)
        {
            Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[3] - 1], transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -45f, 0f)));
            Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[3] - 1], transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 45f, 0f)));

        }

    }


    public void MultiBow()
    {
        Instantiate(PlayerBowShooter, ShooterPoint.transform.position, transform.rotation);
        SoundManager.Instance.PlayerSound("PlayerMutliBowSound", SoundManager.Instance.bgList[12]);
        ////전방으로 같은 방향으로 두발 발사시킴 
        Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[2]], new Vector3(ShooterPoint.transform.position.x, ShooterPoint.transform.position.y, ShooterPoint.transform.position.z), transform.rotation);
   
        if(PlayerSkillData.Instance.BowPlayerSkill[3] > 0)
        {
            Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[3] - 1], transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -45f, 0f)));
            Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[3] - 1], transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 45f, 0f)));
        }


    }

    //공격 메서드를 만들어준다.
    public void Attack()
    {
        PlayerMove.Instance.anim.SetFloat("AttackSpeed", fAttackingSpeed);
        Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[2]], new Vector3(ShooterPoint.transform.position.x, ShooterPoint.transform.position.y - 0.5f, ShooterPoint.transform.position.z), transform.rotation);
        SoundManager.Instance.PlayerSound("CommandSwordSound", SoundManager.Instance.bgList[8]);

        if (PlayerSkillData.Instance.PlayerSkill[1] > 0)
        {
            Invoke("MultiShot", 0.2f);
        }
        if (PlayerSkillData.Instance.PlayerSkill[3] > 0)
        {
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -45f, 0f)));
           
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 45f, 0f)));

        }




    }


    void MultiShot()
    {

        Instantiate(PlayerShooter, ShooterPoint.position, transform.rotation);

        Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[2]], new Vector3(ShooterPoint.transform.position.x, ShooterPoint.transform.position.y, ShooterPoint.transform.position.z), transform.rotation);

        //3번스킬 레벨이 1과 2가 되었을때 

        if (PlayerSkillData.Instance.PlayerSkill[3] > 0)
        {
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -45f, 0f)));
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 45f, 0f)));
        }
        if(PlayerSkillData.Instance.PlayerSkill[3] > 1)
        {
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -45f, 0f)));
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 45f, 0f)));
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -90f, 0f)));
            Instantiate(PlayerSkillData.Instance.PlayerWeapon[PlayerSkillData.Instance.PlayerSkill[3] - 1], ShooterPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 90f, 0f)));

        }

    }




 
    void Targeting()
    {
        //타켓을 선택하기위함이다.

        //만약 몬스터리스트에 몬스터가 있다면
        if (MonsterList.Count != 0)
        {


            fCurrentDistance = 0f;
            fNearDistance = 0f;
            iTargetingIndex = -1;

            for (int i = 0; i < MonsterList.Count; i++)
            {
                //현재 거리에서 몬스터의 거리를 구해준다.
                if (MonsterList[i] != null)
                    fCurrentDistance = Vector3.Distance(transform.position, MonsterList[i].transform.position);


                RaycastHit hit;
                if (MonsterList[i] != null)
                {
                    bool isHiting = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out hit, 100f, layerMask);


                    if (isHiting && hit.transform.CompareTag("Monster"))
                    {
                        if (fTargetingDistance >= fCurrentDistance)
                        {
                            iTargetingIndex = i;

                            fTargetingDistance = fCurrentDistance;
                        
                        MonsterTargetings.SetActive(true);
                        MonsterTargeting.transform.position = new Vector3(MonsterList[iTargetingIndex].transform.position.x,
                            MonsterList[iTargetingIndex].transform.position.y + 5f,
                            MonsterList[iTargetingIndex].transform.position.z);
                    }
                        }


                    if (fNearDistance >= fCurrentDistance)
                    {
                        icloseDistanceIndex = i;

                        fNearDistance = fCurrentDistance;
                    }



                }
            }

            if (iTargetingIndex == -1)
            {
                iTargetingIndex = icloseDistanceIndex;

                              
            }

         

            fNearDistance = Mathf.Infinity;
            fTargetingDistance = Mathf.Infinity;
            bIsTargetIng = true;
        }
    }



    void AnimationAttackTarget()
    {

        if (bIsTargetIng && !JoyStickScript.Instance.isPlayerMoving && MonsterList.Count > 0)
        {

            if (MonsterList[iTargetingIndex] != null)
                transform.LookAt(new Vector3(MonsterList[iTargetingIndex].transform.position.x, transform.position.y, MonsterList[iTargetingIndex].transform.position.z));

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    PlayerMove.Instance.anim.SetBool("Idle", false);
                    PlayerMove.Instance.anim.SetBool("Run", false);
                    PlayerMove.Instance.anim.SetBool("Attack", true);
                    PlayerMove.Instance.anim.SetBool("BowAttack", false);

                }
             
                UICommandWeapon.SetActive(true);
                    UIBowWeapon.SetActive(false);
            }
            else if(Input.GetKeyDown(KeyCode.F2))
            {
                if(PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    PlayerMove.Instance.anim.SetBool("Idle", false);
                    PlayerMove.Instance.anim.SetBool("Run", false);
                    PlayerMove.Instance.anim.SetBool("Attack", false);
                    PlayerMove.Instance.anim.SetBool("BowAttack", true);



                }
                    UICommandWeapon.SetActive(false);
                    UIBowWeapon.SetActive(true);
            }
            

        }
        else if (JoyStickScript.Instance.isPlayerMoving)
        {

            if (!PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                PlayerMove.Instance.anim.SetBool("Idle", false);
                PlayerMove.Instance.anim.SetBool("Run", true);
                PlayerMove.Instance.anim.SetBool("Attack", false);
                PlayerMove.Instance.anim.SetBool("BowAttack", false);
            
            }
        }
        else if (!JoyStickScript.Instance.isPlayerMoving && !bIsTargetIng && MonsterList.Count == 0)
        {
            if (!PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
             
                PlayerMove.Instance.anim.SetBool("Idle", true);
                PlayerMove.Instance.anim.SetBool("Run", false);
                PlayerMove.Instance.anim.SetBool("Attack", false);
                PlayerMove.Instance.anim.SetBool("BowAttack", false);

               
         
            }


        }
      




    }





}


