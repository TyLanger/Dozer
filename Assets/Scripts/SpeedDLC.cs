using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDLC : DLCBlock
{
    protected override void Purchase(int price)
    {
        base.Purchase(price);

        // extend this stuff
        if (player.InDebt())
        {
            // player.curse()
        }

        player.ToggleSpeed();
    }
}
