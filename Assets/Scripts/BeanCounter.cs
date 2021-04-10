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

    Transform cameraTrans;

    static Queue<TextController> standbyQueue;

    public static event Action<int> OnBeanCounted;

    private void Awake()
    {
        cameraTrans = Camera.main.transform;
        if (standbyQueue == null)
        {
            standbyQueue = new Queue<TextController>(10);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bean")
        {
            count += pointValue;
            SpawnBeanText(other.transform.position);
            /*
            if (Time.time > timeLastSpawned + 1)
            {
                timeLastSpawned = Time.time;
                Instantiate(beanText, other.transform.position + Vector3.up*5, Quaternion.identity);
            }
            */
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

    void SpawnBeanText(Vector3 position)
    {
        if(standbyQueue != null)
        {
            if(standbyQueue.Count < 10)
            {
                GameObject copy = Instantiate(beanText, position + Vector3.up * 5, Quaternion.identity);
                TextController textCopy = copy.GetComponent<TextController>();
                standbyQueue.Enqueue(textCopy);
                textCopy.Reset();
            }
            else
            {
                // if I need to spawn more than 10, it should just grab the oldest one and move it
                TextController textCopy = standbyQueue.Dequeue();
                textCopy.transform.position = position + Vector3.up * 5;
                textCopy.transform.rotation = cameraTrans.rotation;
                textCopy.Reset();
                standbyQueue.Enqueue(textCopy);
            }
        }
    }
}
