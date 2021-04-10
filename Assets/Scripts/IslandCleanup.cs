using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCleanup : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
}
