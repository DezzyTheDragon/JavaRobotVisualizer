using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorController : MonoBehaviour
{
    float gizmoSphereSize = 0.05f;
    //float gizmoAxisSize = 0.1f;

    Vector3 vectorMask = Vector3.one;

    public enum axis
    {
        lockX = 0, 
        lockY = 1, 
        lockZ = 2
    }

    [Header("Lock Rotation to axis")]
    [SerializeField] axis axisLock;

    // Start is called before the first frame update
    void Start()
    {
        if (axisLock == axis.lockX)
        {
            vectorMask = new Vector3(1, 0, 0);
        }
        else if (axisLock == axis.lockY)
        {
            vectorMask = new Vector3(0, 1, 0);
        }
        else if (axisLock == axis.lockZ)
        {
            vectorMask = new Vector3(0, 0, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set the angle of the motor
    public void setRotation(float angle)
    {
        Debug.Log(this.gameObject.name + " " + (vectorMask * angle).ToString());
        //Rotate object along its set axis
        this.transform.Rotate((vectorMask * angle));
    }

    public void setSpeed() 
    { 

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, gizmoSphereSize);

        /*if (axisLock == axis.lockX) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(0, gizmoAxisSize, gizmoAxisSize));
        }
        else if (axisLock == axis.lockY) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(gizmoAxisSize, 0, gizmoAxisSize));
        }
        else if (axisLock == axis.lockZ) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(gizmoAxisSize, gizmoAxisSize, 0));
        }*/
    }
}
