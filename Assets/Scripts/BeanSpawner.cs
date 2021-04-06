using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSpawner : MonoBehaviour
{
    public GameObject beanPrefab;
    

    public int numBeans;
    public int startingBeanCount;

    public float burstSpacing;

    // Start is called before the first frame update
    void Start()
    {
        // spawn a bunch of beans
        StartCoroutine(SpawnBeanBurst());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBeanBurst()
    {
        // spawn a bunch of beans, one after another
        // quickly, just enough time to fall out of the way
        for (int i = 0; i < startingBeanCount; i++)
        {
            // randomize rotation someday
            Instantiate(beanPrefab, transform.position, transform.rotation, transform);
            yield return new WaitForSeconds(burstSpacing);
        }
    }
}
