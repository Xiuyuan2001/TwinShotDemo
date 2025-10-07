using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDeadState : EnemyState
{
    EnemyBlack enemy;
    public BlackDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBlack _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        enemy.rb.velocity = Vector2.zero;
        enemy.GetComponent<Collider2D>().enabled = false;

        AudioManager.instance.PlayClip(AudioManager.instance.enemyDead);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (TriggerCalled)
        {
            GameObject.Destroy(enemy.gameObject);
        }
    }
}
