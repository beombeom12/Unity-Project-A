using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterFSM : MonsterBase
{

    public enum ShooterState
    {
        Idle,
        Run,
        Attack
    };

    public ShooterState currentState = ShooterState.Idle;





    public new void Start()
    {
        base.Start();
        parentRoom = transform.parent.transform.parent.gameObject;

        StartCoroutine(FSM());
    }

    protected virtual IEnumerator FSM()
    {
        yield return null;

        while (!parentRoom.GetComponent<RoomChecker>().bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.5f);
        }
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }

    }

    protected virtual void InitMonster()
    {

    }


    protected virtual IEnumerator Idle()
    {
        yield return null;
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Anim.SetTrigger("Idle");
        }

        if (IsAttackReal())
        {
            if (bIsMonsterAttack)
            {
                currentState = ShooterState.Attack;
            }
            else
            {
                currentState = ShooterState.Idle;
                transform.LookAt(Player.transform.position);
            }
        }
        else
        {
            currentState = ShooterState.Run;
        }

    }

    protected virtual void AttackEffect() { }

    protected virtual IEnumerator Attack()
    {
        yield return null;

        NaviAgent.stoppingDistance = 10f;
        NaviAgent.isStopped = true;

        NaviAgent.SetDestination(Player.transform.position);

        yield return new WaitForSeconds(0.5f);

        NaviAgent.isStopped = false;
        NaviAgent.speed = 30f;
        bIsMonsterAttack = false;

        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Anim.SetTrigger("Attack");
        }

        AttackEffect();

        yield return new WaitForSeconds(0.5f);
        NaviAgent.speed = fMoveSpeed;

        NaviAgent.stoppingDistance = fAttackRange;


        currentState = ShooterState.Idle;


    }



    protected virtual IEnumerator Run()
    {

        yield return null;


        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            Anim.SetTrigger("Run");
        }

        if (IsAttackReal() && bIsMonsterAttack)
        {
            currentState = ShooterState.Attack;
        }
        else if (fDistnace > fMonsterFeeling)
        {
            NaviAgent.SetDestination(transform.parent.position - Vector3.forward * 2f);
        }
        else
        {
            NaviAgent.SetDestination(Player.transform.position);
        }

    }



}
