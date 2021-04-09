using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulldozer : MonoBehaviour
{

    public Rigidbody leftTread;
    public Rigidbody rightTread;

    public GameObject leftPlow;
    public GameObject leftPlowBig;
    public GameObject rightPlow;
    public GameObject rightPlowBig;


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
    public Transform spawnPoint;
    bool useNewSpawn = false;

    float minParentScale = 0.3f;
    float maxParentScale = 0.6f;

    public float currentFuelSeconds = 20; // is this working seconds or idle seconds? Working. Idle drains 0.1x fuel?
    public float maxFuelSeconds = 60;
    public float bigTankFuelSeconds = 120;
    public float idleFuelConsumptionScale = 0.1f;

    int goldenBeans = 0;

    bool hornUnlocked = false;

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
            //ToggleSpeed();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Honk();
        }
        if (Input.GetButtonDown("Jump"))
        {
            
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

        // if moving, lose fuel
        currentFuelSeconds -= Mathf.Max(Mathf.Abs(forwardInput), Mathf.Abs(horInput)) * Time.fixedDeltaTime;
        currentFuelSeconds -= idleFuelConsumptionScale * Time.fixedDeltaTime; // lose a little fuel anyway

        if(currentFuelSeconds < 0)
        {
            // explode
        }
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

        if (useNewSpawn)
        {
            transform.position = spawnPoint.position;
        }

    }

    public void SetNewSpawn()
    {
        useNewSpawn = true;
    }

    public void PayCost(int price)
    {
        goldenBeans -= price;
    }

    public bool InDebt()
    {
        return goldenBeans < 0;
    }

    public void ToggleSpeed()
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

    public void MaxSize()
    {
        leftTread.gameObject.SetActive(false);
        rightTread.gameObject.SetActive(false);

        transform.position = transform.position + Vector3.up;
        transform.localScale = Vector3.one * maxParentScale;
        cabOffset = new Vector3(cabOffset.x, cabOffset.y * (maxParentScale / minParentScale), cabOffset.z);

        leftTread.gameObject.SetActive(true);
        rightTread.gameObject.SetActive(true);
    }

    public void BigPlow()
    {
        /*
        leftPlow.SetActive(false);
        rightPlow.SetActive(false);
        leftPlowBig.SetActive(true);
        rightPlowBig.SetActive(true);
        */
    }

    public void UnlockHorn()
    {
        hornUnlocked = true;
    }

    void Honk()
    {
        if(hornUnlocked)
        {
            // sound and a shockwave in front of you
        }
    }

    public void Refuel(float fuel)
    {
        currentFuelSeconds = Mathf.Min(currentFuelSeconds + fuel, maxFuelSeconds);
    }
}
