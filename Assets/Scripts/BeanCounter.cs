using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanCounter : MonoBehaviour
{
    public int count;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bean")
        {
            count++;
        }
    }
}
