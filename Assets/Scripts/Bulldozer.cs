using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulldozer : MonoBehaviour
{

    public Rigidbody leftTread;
    public Rigidbody rightTread;

    public float speed;
    public float turnSpeed;
    public float fastSpeed;
    public float fastTurnSpeed;
    bool slowSpeed = false;

    float currentSpeed;
    float currentTurnSpeed;

    float forwardInput;
    float horInput;

    Vector3 leftStartPos;
    Vector3 rightStartPos;
    Quaternion leftStartRot;
    Quaternion rightStartRot;
    Vector3 cabOffset;
    Rigidbody cabBody;

    // Start is called before the first frame update
    void Start()
    {
        cabOffset = transform.position - (leftTread.position + rightTread.position) / 2;
        cabBody = GetComponent<Rigidbody>();

        leftStartPos = leftTread.position;
        rightStartPos = rightTread.position;
        leftStartRot = leftTread.rotation;
        rightStartRot = rightTread.rotation;

        ToggleSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxisRaw("Vertical");
        horInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            ToggleSpeed();
        }
    }

    private void FixedUpdate()
    {
        //leftTread.AddForce(forwardInput * leftTread.transform.forward * speed * Time.fixedDeltaTime);
        //rightTread.AddForce(forwardInput * rightTread.transform.forward * speed * Time.fixedDeltaTime);
        //leftTread.velocity = forwardInput * leftTread.transform.forward * speed * Time.fixedDeltaTime;
        //rightTread.velocity = forwardInput * rightTread.transform.forward * speed * Time.fixedDeltaTime;
        
        leftTread.MovePosition(leftTread.position + (forwardInput * leftTread.transform.forward * currentSpeed * Time.fixedDeltaTime));
        rightTread.MovePosition(rightTread.position + (forwardInput * rightTread.transform.forward * currentSpeed * Time.fixedDeltaTime));

        leftTread.MovePosition(leftTread.position + (horInput * leftTread.transform.forward * currentTurnSpeed * Time.fixedDeltaTime));
        rightTread.MovePosition(rightTread.position + (-horInput * rightTread.transform.forward * currentTurnSpeed * Time.fixedDeltaTime));

        //transform.position = (leftTread.position + rightTread.position) / 2 + cabOffset;
        //transform.forward = leftTread.transform.forward;
        
        cabBody.MovePosition((leftTread.position + rightTread.position) / 2 + cabOffset);
        cabBody.MoveRotation(leftTread.rotation);
    }

    private void Reset()
    {
        // set kinematic and set postion instead of MovePosition
        // to ignore physics when I reset
        // cab will move into position on its own
        leftTread.isKinematic = true;
        rightTread.isKinematic = true;

        leftTread.position = (leftStartPos);
        rightTread.position = (rightStartPos);
        leftTread.rotation = (leftStartRot);
        rightTread.rotation = (rightStartRot);

        leftTread.isKinematic = false;
        rightTread.isKinematic = false;

    }

    void ToggleSpeed()
    {
        slowSpeed = !slowSpeed;
        if(slowSpeed)
        {
            currentSpeed = speed;
            currentTurnSpeed = turnSpeed;
        }
        else
        {
            // would like to make some force so you are blown back by the speed a bit
            currentSpeed = fastSpeed;
            currentTurnSpeed = fastTurnSpeed;
        }
    }
}
