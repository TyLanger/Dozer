using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeanCounter : MonoBehaviour
{
    
    public static int count;
    public int pointValue = 1; // putting beans in a specific spot gets a bonus

    public GameObject beanText;
    public GameObject tutorialIslandText;
    public GameObject dozerText;

    float timeLastSpawned = 0;

    static Queue<GameObject> standbyQueue;

    public static event Action<int> OnBeanCounted;

    private void Awake()
    {
        /*
        if (standbyQueue == null)
        {
            standbyQueue = new Queue<GameObject>(10);
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bean")
        {
            count += pointValue;
            if (Time.time > timeLastSpawned + 1)
            {
                timeLastSpawned = Time.time;
                Instantiate(beanText, other.transform.position + Vector3.up*5, Quaternion.identity);
            }
            OnBeanCounted?.Invoke(pointValue);
        }
        else if(other.tag == "TutorialIsland")
        {
            Instantiate(tutorialIslandText, other.transform.position + Vector3.up * 5, Quaternion.identity);
        }
        else if(other.tag == "Dozer")
        {
            Instantiate(dozerText, other.transform.position + Vector3.up * 5, Quaternion.identity);
        }
    }
}
