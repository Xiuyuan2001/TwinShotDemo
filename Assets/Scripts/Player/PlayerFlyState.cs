using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyState : PlayerState
{
    public PlayerFlyState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if(Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, player.flyForce);
        }

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
