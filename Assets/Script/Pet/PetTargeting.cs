using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTargeting : MonoBehaviour
{
    public static PetTargeting Instance
    {
        get
        {
            if (PetTargetingInstance == null)
            {
                PetTargetingInstance = FindObjectOfType<PetTargeting>();
                if (PetTargetingInstance == null)
                {
                    var InstanceContainer = new GameObject("PetTargeting");
                    PetTargetingInstance = InstanceContainer.AddComponent<PetTargeting>();
                }
            }
            return PetTargetingInstance;
        }
    }

    private static PetTargeting PetTargetingInstance;


    public List<GameObject> MonsterList = new List<GameObject>();

    //public GameObject PetShooter;

    public Transform ShooterPosition;

    public float fRate;
    public bool bIsTargetIng = false;

    float fCurrentDistance = 0;
    float fNearDistance = 100f;
    float fTargetingDistance = 100f;

    public float fAttackingSpeed = 1f;
    //가까운 인덱스
    public int icloseDistanceIndex = 0;
    public int iTargetingIndex = -1;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Targeting();
        AnimationAttackTarget();
    }



    public void Attack()
    {
        SoundManager.Instance.PlayerSound("PlayerPetAttack", SoundManager.Instance.bgList[49]);
        Instantiate(PlayerSkillData.Instance.PetWeapon, ShooterPosition.position, transform.rotation);
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
        if (bIsTargetIng && MonsterList.Count > 0)
        {
            if (MonsterList[iTargetingIndex] != null)
            {
                transform.LookAt(new Vector3(MonsterList[iTargetingIndex].transform.position.x, transform.position.y, MonsterList[iTargetingIndex].transform.position.z));
            }

            if (!PlayerChaseAttacking.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                PlayerChaseAttacking.Instance.Anim.SetBool("Attack", true);
                PlayerChaseAttacking.Instance.Anim.SetBool("Idle", false);

            }
        }
        else if (MonsterList.Count == 0)
        {
            if (!PlayerChaseAttacking.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                PlayerChaseAttacking.Instance.Anim.SetBool("Idle", true);
                PlayerChaseAttacking.Instance.Anim.SetBool("Attack", false);

            }
        }

            
    }
 }









