using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject FuelTank;
    public float timeBetweenSpawns = 40;
    // fuel recovers 60 fuel which lasts ~60s
    // Spawn every 40s is maybe enough

    bool canSpawnFuel = true;

    public bool isRepeating;

    public static event Action OnFuelInvented;

    private void Start()
    {
        OnFuelInvented += StartSpawningFuel;
    }

    public void FuelInvented()
    {
        OnFuelInvented?.Invoke();
    }

    public void StartSpawningFuel()
    {
        StartCoroutine(SpawnFuel());
    }

    public void StopSpawningFuel()
    {
        canSpawnFuel = false;
    }

    IEnumerator SpawnFuel()
    {
        do
        {
            Instantiate(FuelTank, transform.position, transform.rotation);
            yield return new WaitForSeconds(timeBetweenSpawns);
        } while (canSpawnFuel && isRepeating);
    }
}
