using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlack : Enemy
{
    public BlackMoveState blackMoveState {  get; private set; }
    public BlackJumpState blackJumpState { get; private set; }
    public BlackDeadState blackDeadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        blackMoveState = new BlackMoveState(this,stateMachine,"move",this);
        blackJumpState = new BlackJumpState(this,stateMachine,"jump",this);
        blackDeadState = new BlackDeadState(this,stateMachine,"dead",this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(blackMoveState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
