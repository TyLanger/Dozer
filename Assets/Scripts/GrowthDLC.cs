using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthDLC : DLCBlock
{
    protected override void Purchase(int price)
    {
        base.Purchase(price);

        if (player.InDebt())
        {
            // player.curse()
        }

        //player.MaxSize();
        player.BigPlow();
    }
}
