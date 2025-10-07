using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    public override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }

    public override void Die()
    {
        base.Die();

        // 通知EnemyManager敌人死亡
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.OnEnemyDeath(gameObject);
        }

        if (enemy != null && enemy.itemDrop != null)
        {
            enemy.GetComponent<Collider2D>().enabled = false;
            enemy.itemDrop.GenerateDrop();
            //Debug.Log("DROP!");
        }
    }

    public override void Update()
    {
        base.Update();

    }

    public override void DoDamage(CharacterStats _targetStats)
    {
        base.DoDamage(_targetStats);
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }
}
