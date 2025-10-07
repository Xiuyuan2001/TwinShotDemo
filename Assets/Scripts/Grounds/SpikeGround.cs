using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGround : MonoBehaviour
{
    [Header("Damage Info")]
    [SerializeField] private int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
