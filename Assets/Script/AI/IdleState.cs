using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IdleState : State
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager._parameter;
    }
    public void OnEnter()
    {
        //parameter._animator.Play("idle");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}
///////////////////////////////////////////////////////////
public class PatrolState : State
{
    private FSM manager;
    private Parameter parameter;
    private int patorlPosition;
    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager._parameter;
    }
    public void OnEnter()
    {
        parameter._animator.Play("move");
    }

    public void OnUpdate()
    {
        manager.FilpTo(parameter.patrolPoints[patorlPosition]);

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patorlPosition].position, parameter.moveSpeed * Time.deltaTime);

        if(Vector2.Distance(manager.transform.position, parameter.patrolPoints[patorlPosition].position) < .1f)
        {
            manager.TransitionState(StateType.Idle);
        }

        
    }

    public void OnExit()
    {
        patorlPosition++;

        if (patorlPosition >= parameter.patrolPoints.Length)
        {
            patorlPosition = 0;
        }
    }
}
///////////////////////////////////////////////////////////
public class ChaseState : State
{
    private FSM manager;
    private Parameter parameter;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager._parameter;
    }
    public void OnEnter()
    {
        parameter._animator.Play("move");
    }

    public void OnUpdate()
    {
        manager.FilpTo(parameter.target);
        if(parameter.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                parameter.target.position, parameter.chaseSpeed * Time.deltaTime);
        }
        if(parameter.target == null ||
            manager.transform.position.x < parameter.chasePoints[0].position.x ||
            manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }
        if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLyaer))
        {
            manager.TransitionState(StateType.Attack);
        }
    }

    public void OnExit()
    {
        
    }
}