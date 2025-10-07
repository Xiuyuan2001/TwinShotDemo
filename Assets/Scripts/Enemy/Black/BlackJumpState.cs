using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJumpState : EnemyState
{
    EnemyBlack enemy;

    private float jumpForce = 8f;

    public BlackJumpState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBlack _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.blackMoveState);
    }
}
