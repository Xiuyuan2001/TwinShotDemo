using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFly : Item
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
        {
            player.canFly = true;
            player.isSpeeding = false;
            player.isInvincible = false;
        }
    }

    protected override IEnumerator OnPickup()
    {
        return base.OnPickup();
    }
}
