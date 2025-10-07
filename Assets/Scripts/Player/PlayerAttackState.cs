using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    [SerializeField] private float arrowSpawnDelay = 0.5f;
    private bool arrowSpawned = false;

    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        arrowSpawned = false;
        player.StartCoroutine(SpawnArrowWithDelay());

    }

    private IEnumerator SpawnArrowWithDelay()
    {
        yield return new WaitForSeconds(arrowSpawnDelay);

        AudioManager.instance.PlayClip(AudioManager.instance.attack);

        if (!arrowSpawned && stateMachine.currentState == this)
        {
            SkillManager.instance.arrow.CreateArrow();
            arrowSpawned = true;
        }
    }
    public override void Update()
    {
        base.Update();

        if(TriggerCalled)
            stateMachine.ChangeState(player.idleState);

        if(!UI_Pause.isPause && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 10);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
