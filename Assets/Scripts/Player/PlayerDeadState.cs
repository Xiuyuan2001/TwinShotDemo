using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        
        player.anim.speed = 0;
        player.rb.velocity = Vector2.zero;
        player.coll.enabled = false;
    }
    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
