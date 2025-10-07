using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyElephant : Enemy
{
    public FlyElephantMoveState flyElephantMoveState { get; private set; }
    public FlyElephantDeadState flyElephantDeadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        flyElephantMoveState = new FlyElephantMoveState(this, stateMachine, "move", this);
        flyElephantDeadState = new FlyElephantDeadState(this, stateMachine, "dead", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(flyElephantMoveState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
