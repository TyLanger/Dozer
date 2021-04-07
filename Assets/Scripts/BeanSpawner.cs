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
    Queue<GameObject> standbyQueue;

    float minBurstSpacing = 0.2f;
    float maxBurstSpacing = 0.8f;

    float minTrickleSpacing = 2;
    float maxTrickleSpacing = 4;

    int minBurstSize = 16;
    int maxBurstSize = 30;

    int minTrickleSize = 4;
    int maxTrickleSize = 8;

    float minBeanScale = 0.1f;
    float maxBeanScale = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        standbyQueue = new Queue<GameObject>(100);

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
            SpawnBean();
            yield return new WaitForSeconds(timeBetweenBeans);
        }
    }

    void SpawnBean()
    {
        GameObject copy;
        if (standbyQueue.Count == 0)
        {
            copy = Instantiate(beanPrefab, transform.position, transform.rotation, transform);
            copy.transform.localScale = Vector3.one * Random.Range(minBeanScale, maxBeanScale);
        }
        else
        {
            copy = standbyQueue.Dequeue();
            copy.transform.position = transform.position;
            //Debug.Log("Used from queue");
        }
        

        BeanCleanup script = copy.GetComponent<BeanCleanup>();
        script.OnLeaveWorld -= BeanLeftWorld;
        script.OnLeaveWorld += BeanLeftWorld;
        script.StartPhysics();
    }

    void BeanLeftWorld(GameObject bean)
    {
        // add to standby queue
        standbyQueue.Enqueue(bean);
        bean.transform.position = new Vector3(0, 0, -50); // off screen
        //BeanCleanup script = bean.GetComponent<BeanCleanup>();
        //Debug.Log("Put bean away");
        
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
