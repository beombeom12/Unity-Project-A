using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPetTargeting : MonoBehaviour
{
    public static BatPetTargeting Instance
    {
        get
        {
            if (BatPetTargetingInstance == null)
            {
                BatPetTargetingInstance = FindObjectOfType<BatPetTargeting>();
                if (BatPetTargetingInstance == null)
                {
                    var InstanceContainer = new GameObject("BatPetTargeting");
                    BatPetTargetingInstance = InstanceContainer.AddComponent<BatPetTargeting>();
                }
            }
            return BatPetTargetingInstance;
        }
    }
    private static BatPetTargeting BatPetTargetingInstance;





    //�߰��� ���� ����Ʈ
    public List<GameObject> MonsterList = new List<GameObject>();
    //��� ��ġ
    public GameObject ShooterPosition;
    //���� ��� �Ѿ� ������
    public GameObject ShotWeapon;


    public float fRate;
    public bool bIsTargetIng = false;

    float fCurrentDistance = 0;
    float fNearDistance = 100f;
    float fTargetingDistance = 100f;

    public float fAttackingSpeed = 1f;
    //����� �ε���
    public int icloseDistanceIndex = 0;
    public int iTargetingIndex = -1;

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


    // Start is called before the first frame update
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
        SoundManager.Instance.PlayerSound("BatPetSound", SoundManager.Instance.bgList[8]);
        Instantiate(ShotWeapon, ShooterPosition.transform.position, transform.rotation);
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



    public void AnimationAttackTarget()
    {
        if (bIsTargetIng && MonsterList.Count > 0)
        {
            if (MonsterList[iTargetingIndex] != null)
            {
                transform.LookAt(new Vector3(MonsterList[iTargetingIndex].transform.position.x, transform.position.y, MonsterList[iTargetingIndex].transform.position.z));

            }


            if (!BatPetChasing.Instance.BatPetAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                BatPetChasing.Instance.BatPetAnim.SetBool("Idle", false);
                BatPetChasing.Instance.BatPetAnim.SetBool("Attack", true);
            }
        }
        else if (MonsterList.Count == 0)
        {
             if (!BatPetChasing.Instance.BatPetAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                BatPetChasing.Instance.BatPetAnim.SetBool("Idle", true);
                BatPetChasing.Instance.BatPetAnim.SetBool("Attack",false);

            }
        }
    }
}
