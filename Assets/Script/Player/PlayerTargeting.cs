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


    //���͸� �켱 �������ִ� ����Ʈ ����
    public List<GameObject> MonsterList = new List<GameObject>();

    //���߿� ������ ����
    public GameObject PlayerShooter;

    //Ȱ
    public GameObject PlayerBowShooter;


    //UIȰ�� ǥâ

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
    //����� �ε���
    public int icloseDistanceIndex = 0;
    public int iTargetingIndex = -1;



    //���̾ �����ؼ� �ڱ��ڽ��� �����ϰų� ������ �������� �ʱ� ���ؼ�
    public LayerMask layerMask;



    public float fDelayTime = 3f;


    private void OnDrawGizmos()
    {
        //Ÿ�������̰� ������ �������� ����
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
    //�������� �ι߾� ��°�[1]
        if(PlayerSkillData.Instance.BowPlayerSkill[1] > 0)
        {
            Invoke("MultiBow", 0.2f);
        }

        //�缱�������� ���ư��� ȭ��[3]
        //0 �ϰ�� 45 �� �ΰ�
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
        ////�������� ���� �������� �ι� �߻��Ŵ 
        Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[2]], new Vector3(ShooterPoint.transform.position.x, ShooterPoint.transform.position.y, ShooterPoint.transform.position.z), transform.rotation);
   
        if(PlayerSkillData.Instance.BowPlayerSkill[3] > 0)
        {
            Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[3] - 1], transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -45f, 0f)));
            Instantiate(PlayerSkillData.Instance.PlayerBowWeapon[PlayerSkillData.Instance.BowPlayerSkill[3] - 1], transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 45f, 0f)));
        }


    }

    //���� �޼��带 ������ش�.
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

        //3����ų ������ 1�� 2�� �Ǿ����� 

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
        //Ÿ���� �����ϱ������̴�.

        //���� ���͸���Ʈ�� ���Ͱ� �ִٸ�
        if (MonsterList.Count != 0)
        {


            fCurrentDistance = 0f;
            fNearDistance = 0f;
            iTargetingIndex = -1;

            for (int i = 0; i < MonsterList.Count; i++)
            {
                //���� �Ÿ����� ������ �Ÿ��� �����ش�.
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


