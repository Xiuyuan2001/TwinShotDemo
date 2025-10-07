using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (!UI_Pause.isPause)
        {
            if(Input.GetKeyDown(KeyCode.W) && player.IsGroundDetected())
                stateMachine.ChangeState(player.jumpState);

            if(Input.GetKeyDown(KeyCode.Space) && !player.isHurt)
                stateMachine.ChangeState(player.attackState);
        }

        if(!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
