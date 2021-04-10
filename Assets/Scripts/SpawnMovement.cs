using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnMovement : MonoBehaviour
{

    public Transform endPosition;
    bool startedMoving = false;
    public float speed = 1;

    public UnityEvent startTriggerEvent;
    public UnityEvent endTriggerEvent;

    // Start is called before the first frame update
    void Start()
    {
        startTriggerEvent?.Invoke();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(startedMoving)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition.position, Time.fixedDeltaTime * speed);
            if(Vector3.Distance(transform.position, endPosition.position) < 0.01f)
            {
                startedMoving = false;
                ReachedDestination();
            }
        }
    }

    void ReachedDestination()
    {
        endTriggerEvent?.Invoke();
    }

    public void MoveToPosition()
    {
        startedMoving = true;
    }

    public void SetMoveOnVisible()
    {
        Director.OnGameVisible += MoveToPosition;
    }
}
