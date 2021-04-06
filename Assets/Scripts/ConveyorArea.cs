using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorArea : MonoBehaviour
{
    public float beltSpeed = 1;

    private void OnTriggerStay(Collider other)
    {
        
        Rigidbody rbody = other.GetComponent<Rigidbody>();
        if(rbody)
        {
            // doesn't launch stuff, but otherwise works
            // upside is that this conveyor can be pushed around while it's running
            rbody.MovePosition(rbody.position + (transform.forward * beltSpeed * Time.deltaTime));
            
        }
    }
}
