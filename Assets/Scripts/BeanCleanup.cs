using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanCleanup : MonoBehaviour
{
    public event Action<GameObject> OnLeaveWorld;

    float bottomOfWorld = -12f;

    Rigidbody rbody;

    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < bottomOfWorld)
        {
            EndPhysics();
            OnLeaveWorld?.Invoke(gameObject);
        }
    }

    public void StartPhysics()
    {
        rbody.useGravity = true;
        gameObject.GetComponent<Collider>().enabled = true;

    }

    void EndPhysics()
    {
        
        gameObject.GetComponent<Collider>().enabled = false;
        rbody.useGravity = false;
        rbody.velocity = Vector3.zero;
        rbody.angularVelocity = Vector3.zero;
    }
}
