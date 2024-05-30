using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class MotorController : MonoBehaviour
{
    //TODO: Add a queue system to buffer commands

    [Header("Rotation Behavior Values")]
    [SerializeField] float springForce = 0;
    [SerializeField] float damperForce = 0;
    [Header("Speed Behavior Values")]
    [SerializeField] float motorForce = 0;

    HingeJoint joint;
    JointMotor motor;
    JointSpring spring;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<HingeJoint>();
        motor = new JointMotor();
        motor.force = motorForce;
        spring = new JointSpring();
        spring.spring = springForce;
        spring.damper = damperForce;
    }

    //Utalizes physics so we need to use FixedUpdate
    private void FixedUpdate()
    {
    }

    //Set the angle of the motor
    public void setRotation(float angle)
    {
        //Debug.Log(this.gameObject.name + " attempting to reach " +  angle);

        spring.targetPosition = angle;
        joint.useMotor = false;
        joint.useSpring = true;
        joint.spring = spring;
    }

    public void setSpeed(float speed) 
    {
        motor.targetVelocity = speed;
        joint.useSpring = false;
        joint.useMotor = true;
        joint.motor = motor;
    }

}
