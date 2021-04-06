using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    Rigidbody rbody;
    public float speed = 1;

    public Transform leftEdge;
    public Transform rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            // turn off the conveyor so you can push it
            rbody.isKinematic = !rbody.isKinematic;
        }
    }

    void FixedUpdate()
    {
        // pushes stuff on the conveyor
        // launches it when it leaves

        Vector3 pos = rbody.position;
        rbody.position += -transform.forward * speed * Time.fixedDeltaTime;
        rbody.MovePosition(pos);
    }

}
