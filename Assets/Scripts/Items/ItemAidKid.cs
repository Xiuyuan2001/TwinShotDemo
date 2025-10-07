using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAidKid : Item
{
    protected override IEnumerator OnPickup()
    {
        yield return null;

        base.OnPickup();

        if(player.playerStats.currentHealth < 3)
            player.playerStats.currentHealth++;

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}
