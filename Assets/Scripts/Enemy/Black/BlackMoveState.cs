using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMoveState : EnemyState
{
    EnemyBlack enemy;

    private float jumpTimer;
    private float jumpDuration = 4f;

    public BlackMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName , EnemyBlack _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        jumpTimer = jumpDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        jumpTimer -= Time.deltaTime;

        if (enemy == null || enemy.gameObject == null)
            return;

        if (enemy.enemyStats.currentHealth <= 0)
        {
            stateMachine.ChangeState(enemy.blackDeadState);
            return;
        }

        if (enemy.IsWallDetected())
            enemy.Flip();

        if (enemy.facingRight)
            enemy.rb.velocity = new Vector2(enemy.moveSpeed, enemy.rb.velocity.y);
        else
            enemy.rb.velocity = new Vector2(-enemy.moveSpeed, enemy.rb.velocity.y);

        if(jumpTimer <= 0)
        {
            jumpTimer = jumpDuration;
            stateMachine.ChangeState(enemy.blackJumpState);
        }
    }
}
