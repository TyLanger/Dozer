using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanCounter : MonoBehaviour
{
    
    public static int count;
    public int pointValue = 1; // putting beans in a specific spot gets a bonus

    public GameObject beanText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bean")
        {
            count += pointValue;
            Instantiate(beanText, other.transform.position + Vector3.up*5, Quaternion.identity);
        }
    }
}
