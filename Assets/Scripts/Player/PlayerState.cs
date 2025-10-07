using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected string animBoolName;

    public Rigidbody2D rb;

    public float xInput;
    public float yInput;
    public float yVelocity;

    public bool TriggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);  

        rb = player.rb;

        TriggerCalled = false;
    }

    public virtual void Update()
    {
        if (!UI_Pause.isPause)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            xInput = 0;
            yInput = 0;
        }

        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void AnimationFinishTrigger()
    {
        TriggerCalled = true;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
