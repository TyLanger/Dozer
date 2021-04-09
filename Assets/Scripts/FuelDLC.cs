using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelDLC : DLCBlock
{
    ParticleSystem particles;

    public float fuel = 60;
    public float playerSuckMultiplier = 2;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        
    }

    protected override void Purchase(int price)
    {
        base.Purchase(price);

        particles.Play();
        StartCoroutine(StartDraining());
        // extend this stuff
        if (player.InDebt())
        {
            // player.curse()
        }

        //player.Refuel(fuel);
    }

    IEnumerator StartDraining()
    {
        float startEmissionRate = particles.emission.rateOverTime.constant;
        float startFuel = fuel;
        var emission = particles.emission;
        while (fuel > 0)
        {
            yield return null;
            fuel -= Time.deltaTime;
            emission.rateOverTime = startEmissionRate * (fuel/startFuel);
        }
        particles.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        if (fuel > 0)
        {
            if (player)
            {
                // player isn't set until this is activated. Which is what I want anyway
                if (other.gameObject == player.gameObject)
                {
                    fuel -= Time.deltaTime * playerSuckMultiplier;
                    player.Refuel(Time.deltaTime * playerSuckMultiplier);
                }
            }
        }
    }
}
