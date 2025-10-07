using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected string animBoolName;

    protected Rigidbody2D rb;

    public float stateTimer;

    protected bool TriggerCalled;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        enemyBase = _enemy;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        TriggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void AnimationFinishTrigger()
    {
        TriggerCalled = true;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }
}
