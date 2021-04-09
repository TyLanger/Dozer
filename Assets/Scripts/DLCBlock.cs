using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DLCBlock : MonoBehaviour
{
    // when you push this over, you buy something
    // how do I set what gets unlocked?
    // unlock class that I extend
    // Or I can extend this

    public event Action OnPurchase;

    public int price = 0;
    public int backPrice = 0;

    bool bought = false;

    protected Bulldozer player;

    float worldEdge = -12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < worldEdge)
        {
            Destroy(gameObject);
        }

        if (!bought)
        {
            // using euler, good is 90
            // bad is 270
            // wiggle a bit and you hit 360
            float angle = transform.rotation.eulerAngles.x;
            if (angle > 80 && angle < 100)
            {
                // good
                bought = true;
                //Debug.Log($"Paid front price for {gameObject.name}");
                Purchase(price);
            }
            else if(angle > 260 && angle < 280)
            {
                bought = true;
                //Debug.Log($"Paid back price for {gameObject.name}");
                Purchase(backPrice);
            }
        }
    }

    protected virtual void Purchase(int price)
    {
        // find player
        // decrement their money
        // if they go into debt, uh oh
        // player.MaxScale
        // player.MaxSpeed

        player = FindObjectOfType<Bulldozer>();
        player.PayCost(price);


        // extend this stuff
        /*
        if(player.InDebt())
        {
            // player.curse()
        }

        player.MaxSize();
        */
    }


}
