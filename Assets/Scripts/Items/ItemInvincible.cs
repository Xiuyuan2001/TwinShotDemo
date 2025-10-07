using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvincible : Item
{
    protected override void Start()
    {
        base.Start();

        scoreValue = 200;
    }

    protected override void Update()
    {
        base.Update();

        if (isPicked)
            player.isInvincible = true;

    }

    protected override IEnumerator OnPickup()
    {
        return base.OnPickup();
    }
}
