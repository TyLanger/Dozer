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

    public static event Action<int> OnBeanCounted;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bean")
        {
            count += pointValue;
            Instantiate(beanText, other.transform.position + Vector3.up*5, Quaternion.identity);
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
