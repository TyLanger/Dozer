using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform jarAnchor;
    public Transform dozer;

    public float minFocusLength;
    public float maxFocusLength;
    public Vector3 posOffset;
    public float viewAngle = 45;

    public Transform debugLookPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 anchorToDozerRay = new Vector3(dozer.position.x - jarAnchor.position.x, 0, dozer.position.z - jarAnchor.position.z);

        // look at
        // should be the center of the screen
        Vector3 focusPoint = jarAnchor.position + anchorToDozerRay.normalized * Mathf.Clamp(anchorToDozerRay.magnitude, minFocusLength, maxFocusLength);

        debugLookPoint.position = focusPoint;
        debugLookPoint.right = anchorToDozerRay.normalized;
        //debugLookPoint.forward = jarAnchor.position + anchorToDozerRay.normalized * focusLength - debugLookPoint.position;

        //transform.position = Vector3.Lerp(transform.position, focusPoint + posOffset, Time.deltaTime);
        transform.right = Vector3.Lerp(transform.right, -anchorToDozerRay.normalized, Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, focusPoint + transform.forward * posOffset.z + transform.up * posOffset.y, Time.deltaTime);

        transform.RotateAround(transform.right, viewAngle);
        //transform.Rotate(transform.right, viewAngle);
        //transform.up = origUp;
        //transform.forward = jarAnchor.position + anchorToDozerRay.normalized * focusLength - transform.position;
        //transform.LookAt(jarAnchor.position + anchorToDozerRay.normalized * focusLength);

    }
}
