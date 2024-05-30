using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] float angleMin = -85;
    [SerializeField] float angleMax = 85;
    [SerializeField] private float camDist = 4;

    private float sensitivity = 1;

    
    private float xRot;
    private float yRot;

    private Vector2 delta;
    private bool isRotating;

    
    //Mask out the robot so ray does not collide with it
    private int layerMask = ~(1 << 8);
    //This pulls the camera closer to avoid cliping
    private float distBuffer = 0.5f;

    private void Start()
    {
        xRot = transform.localRotation.eulerAngles.y;
        yRot = transform.localRotation.eulerAngles.x;
    }

    private void LateUpdate()
    {
        //detect camera collision
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * camDist, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, camDist, layerMask)) 
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name + " at " + hit.transform.ToString());
            cam.transform.localPosition = new Vector3(0, 0, -(hit.distance - distBuffer));
        }
        else
        {
            cam.transform.localPosition = new Vector3(0, 0, -(camDist - distBuffer));
        }
        //Rotate the camera
        if (isRotating)
        {
            yRot += (delta.y * sensitivity);
            xRot += (delta.x * sensitivity);

            yRot = Mathf.Clamp(yRot, angleMin, angleMax);

            Vector3 rot = new Vector3(yRot, xRot, 0);

            transform.eulerAngles = rot;

        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        delta = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        isRotating = context.started || context.performed;
    }
}
