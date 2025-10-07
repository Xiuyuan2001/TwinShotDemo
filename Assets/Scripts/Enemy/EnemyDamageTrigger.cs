using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    private EnemyStats enemyStats;

    [Header("Damage Settings")]
    public float damageCooldown = 1f;
    private float lastDamageTime;

    private void Start()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamagePlayer(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DamagePlayer(collision);
    }

    private void DamagePlayer(Collider2D collision)
    {
        if (enemyStats == null) return;

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    enemyStats.DoDamage(playerStats);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}