using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFSM : MonsterBase
{
    public enum GhostState
    {

        Idle,
        Run,
        Attack
    };

    public GhostState currentState = GhostState.Idle;
    
    
    
    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);

    WaitForSeconds Delay200 = new WaitForSeconds(0.2f);




    // Start is called before the first frame update
    public new void  Start()
    {
        base.Start();
        parentRoom = transform.parent.transform.parent.gameObject;

        StartCoroutine(FSM());
    }
    protected virtual void InitMonster() { }

    protected virtual IEnumerator FSM()
    {
        yield return null;

        while (!parentRoom.GetComponent<RoomChecker>().bRoomEnterPlayer)
        {
            yield return Delay500;
        }
        InitMonster();

        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }

    }

    protected virtual IEnumerator Idle()
    {
        yield return null;
        if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Anim.SetTrigger("Idle");
        }

        if(IsAttackReal())
        {
            if(bIsMonsterAttack)
            {
                currentState = GhostState.Attack;
            }
            else
            {
                currentState = GhostState.Idle;
                transform.LookAt(Player.transform.position);
            }
        }
        else
        {
            currentState = GhostState.Run;
        }

    }

    protected virtual void AttackEffect() { }

    protected virtual IEnumerator Attack()
    {
        yield return null;

        NaviAgent.stoppingDistance = 0.1f;
        NaviAgent.isStopped = true;

        NaviAgent.SetDestination(Player.transform.position);

        yield return Delay500;

        NaviAgent.isStopped = false;
        NaviAgent.speed = 30f;
        bIsMonsterAttack = false;

        if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Anim.SetTrigger("Attack");
        }

        AttackEffect();

        yield return Delay500;
        NaviAgent.speed = fMoveSpeed;

        NaviAgent.stoppingDistance = fAttackRange;


        currentState = GhostState.Idle;


    }



    protected virtual IEnumerator Run()
    {

        yield return null;


        if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            Anim.SetTrigger("Run");
        }

        if(IsAttackReal() && bIsMonsterAttack)
        {
            currentState = GhostState.Attack;
        }
        else if(fDistnace > fMonsterFeeling)
        {
            NaviAgent.SetDestination(transform.parent.position - Vector3.forward * 1f);
        }
        else
        {
            NaviAgent.SetDestination(Player.transform.position);
        }

    }



}
