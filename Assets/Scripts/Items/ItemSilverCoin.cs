using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSilverCoin : Item
{
    public CircleCollider2D cc;

    protected override void Start()
    {
        base.Start();
        cc = GetComponentInChildren<CircleCollider2D>();

        scoreValue = 50;
    }

    protected override IEnumerator OnPickup()
    {
        yield return null;

        AudioManager.instance.PlayClip(AudioManager.instance.pickUpCoin);

        if (UI_Score.instance != null)
        {
            UI_Score.instance.AddSilverCoin();
        }

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}
