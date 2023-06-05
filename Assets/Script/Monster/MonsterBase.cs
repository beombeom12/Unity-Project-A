using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{


    //Base
    public float fMaxHp = 1000f;
    public float fCurrenHp = 1000f;
    public float MonsterDamage = 100f;





    protected float fMonsterFeeling = 10f;

    protected float fAttackRange = 5f;
    protected float fAttackCoolTime = 5f;
    protected float fAttackCalculator = 5f;
    protected bool bIsMonsterAttack = true;


    protected float fMoveSpeed = 2f;

    protected GameObject Player;
    protected NavMeshAgent NaviAgent;
    protected float fDistnace;


    protected GameObject parentRoom;

    protected Animator Anim;

    protected Rigidbody MonsterRigidBody;

    public LayerMask layerMask;


    // Start is called before the first frame update
    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        NaviAgent = GetComponent<NavMeshAgent>();

        MonsterRigidBody = GetComponent<Rigidbody>();

        Anim = GetComponent<Animator>();


        parentRoom = transform.parent.transform.parent.gameObject;


        StartCoroutine(CalculatorCoolTime());

    }

    protected bool IsAttackReal()
    {
        Vector3 TargetDistance = new Vector3(Player.transform.position.x - transform.position.x, 0f, Player.transform.position.z - transform.position.z);

        Physics.Raycast(new Vector3(transform.position.x, 1f, transform.position.z), TargetDistance, out RaycastHit hit, 30f, layerMask);


        fDistnace = Vector3.Distance(Player.transform.position, transform.position);

        if(hit.transform == null)
        {
            return false;
        }

        if(hit.transform.CompareTag("Player") && fDistnace <= fAttackRange)
        {

            return true;
        }
        else
        {
            return false;
        }


    }




    protected virtual IEnumerator CalculatorCoolTime()
    {
        while(true)
        {
            yield return null;
            if(!bIsMonsterAttack)
            {
                fAttackCalculator -= Time.deltaTime;


                if(fAttackCalculator <= 0)
                {
                    fAttackCalculator = fAttackCoolTime;
                    bIsMonsterAttack = true;
                }
            }
        }
    }


}
