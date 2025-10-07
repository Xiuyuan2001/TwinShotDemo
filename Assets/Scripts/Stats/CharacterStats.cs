using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major Stats")]
    public int health;
    public int attackDamage;
    public int currentHealth;

    public bool isDead;

    [HideInInspector] public Player player;

    public virtual void Awake()
    {
        player = PlayerManager.instance.player; 

        currentHealth = health;
    }

    public virtual void Update()
    {
        
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        _targetStats.TakeDamage(attackDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
    }
}
