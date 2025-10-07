using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlim : Enemy
{
    public SlimMoveState slimMoveState { get; private set; }
    public SlimDeadState slimDeadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        slimMoveState = new SlimMoveState(this, stateMachine, "move", this);
        slimDeadState = new SlimDeadState(this, stateMachine, "dead", this);
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(slimMoveState);
    }

    protected override void Update()
    {
        base.Update();
    }

}
