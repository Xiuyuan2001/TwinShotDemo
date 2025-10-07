using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimMoveState : EnemyState
{
    private EnemySlim enemy;

    public SlimMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName , EnemySlim _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy == null || enemy.gameObject == null)
            return;

        if(enemy.enemyStats.currentHealth <= 0)
        {
            stateMachine.ChangeState(enemy.slimDeadState);
            return;
        }

        if (!enemy.IsGroundDetected() || enemy.IsWallDetected())
            enemy.Flip();

        if (enemy.facingRight)
            enemy.rb.velocity = new Vector2(enemy.moveSpeed, enemy.rb.velocity.y);
        else
            enemy.rb.velocity = new Vector2(-enemy.moveSpeed, enemy.rb.velocity.y);
    }
}
