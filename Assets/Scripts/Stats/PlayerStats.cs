using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {

    }

    public void ResetStats()
    {
        isDead = false;
        player.anim.speed = 1;
        player.anim.SetBool("dead", false);
        player.coll.enabled = true;
        currentHealth = health;

        player.stateMachine.ChangeState(player.idleState);
    }

    public override void DoDamage(CharacterStats _targetStats)
    {
        base.DoDamage(_targetStats);
    }

    public override void TakeDamage(int _damage)
    { 
        if (player.isInvincible)
            return;

        base.TakeDamage(_damage);

        AudioManager.instance.PlayClip(AudioManager.instance.hurt);

        player.DamageVisualEffect();
    
    }

    public override void Die()
    {
        base.Die();

        player.stateMachine.ChangeState(player.deadState);
    }

}
