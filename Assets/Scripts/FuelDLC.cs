using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelDLC : DLCBlock
{

    public float fuel = 60; 

    protected override void Purchase(int price)
    {
        base.Purchase(price);

        // extend this stuff
        if (player.InDebt())
        {
            // player.curse()
        }

        player.Refuel(fuel);
    }
}
