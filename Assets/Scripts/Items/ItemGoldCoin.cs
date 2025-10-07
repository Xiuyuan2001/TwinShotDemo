using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGoldCoin : Item
{
    public CircleCollider2D cc;

    protected override void Start()
    {
        base.Start();
        cc = GetComponentInChildren<CircleCollider2D>();

        scoreValue = 100;
    }

    protected override IEnumerator OnPickup()
    { 
        yield return null;

        AudioManager.instance.PlayClip(AudioManager.instance.pickUpCoin);

        if (UI_Score.instance != null)
        {
            UI_Score.instance.AddGoldCoin();
        }

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }  
}