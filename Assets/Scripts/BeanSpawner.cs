using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSpawner : MonoBehaviour
{
    public GameObject beanPrefab;
    // How do I make a pool?
    // queue of idle beans
    // when a bean falls off, add it to the idle queue
    // if the queue count > 0, grab from there instead of Instantiate?
    // I might not make enough beans quick enough to warrant it

    float minBurstSpacing = 0.2f;
    float maxBurstSpacing = 0.8f;

    float minTrickleSpacing = 2;
    float maxTrickleSpacing = 4;

    int minBurstSize = 16;
    int maxBurstSize = 30;

    int minTrickleSize = 4;
    int maxTrickleSize = 8;

    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(AlternateSpawns());
    }

    IEnumerator AlternateSpawns()
    {
        // burst
        yield return StartCoroutine(SpawnBeansOverTime(GetRandom(minBurstSize, maxBurstSize), GetRandom(minBurstSpacing, maxBurstSpacing)));

        // trickle
        yield return StartCoroutine(SpawnBeansOverTime(GetRandom(minTrickleSize, maxTrickleSize), GetRandom(minTrickleSpacing, maxTrickleSpacing)));

        // repeat
        StartCoroutine(AlternateSpawns());
    }

    IEnumerator SpawnBeansOverTime(int beansToSpawn, float timeBetweenBeans)
    {
        for (int i = 0; i < beansToSpawn; i++)
        {
            // randomize rotation someday
            Instantiate(beanPrefab, transform.position, transform.rotation, transform);
            yield return new WaitForSeconds(timeBetweenBeans);
        }
    }

    float GetRandom(float min, float max)
    {
        // gives a distibution like summing 2 dice
        float r1 = Random.Range(min / 2, max / 2);
        float r2 = Random.Range(min / 2, max / 2);
        return r1 + r2;
    }

    int GetRandom(int min, int max)
    {
        int r1 = Random.Range(min / 2, max / 2);
        int r2 = Random.Range(min / 2, max / 2);
        return r1 + r2;
    }
}
