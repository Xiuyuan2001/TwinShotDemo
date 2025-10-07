using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);

        AudioManager.instance.PlayClip(AudioManager.instance.jump);
    }
    public override void Update()
    {
        base.Update();

        if(yVelocity < 0)
            stateMachine.ChangeState(player.airState);

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);

        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if(!UI_Pause.isPause && Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(player.attackState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
