﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Provides an easy way to controll hinge joint spring and motor
[RequireComponent(typeof(HingeJoint))]
public class MotorController : MonoBehaviour
{
    protected enum EncoderAxis { X_AXIS, Y_AXIS, Z_AXIS }

    //TODO: Add a queue system to buffer commands

    [Header("Controller Settings")]
    [SerializeField] protected EncoderAxis encoderAxis = EncoderAxis.X_AXIS;
    [Header("Rotation Behavior Values")]
    [SerializeField] protected float springForce = 0;
    [SerializeField] protected float damperForce = 0;
    [Header("Speed Behavior Values")]
    [SerializeField] protected float motorForce = 0;

    private HingeJoint joint;
    protected JointMotor motor;
    protected JointSpring spring;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Motor Controller: " + gameObject.name);

        joint = GetComponent<HingeJoint>();
        initJoint();

        //Default motor behavior is maintain default position
        setRotation(0);
    }

    //Utalizes physics so we need to use FixedUpdate
    private void FixedUpdate()
    {
    }

    //Initializes the template motor and springs
    protected void initJoint() 
    {
        motor = new JointMotor();
        motor.force = motorForce;
        spring = new JointSpring();
        spring.spring = springForce;
        spring.damper = damperForce;
    }

    //Set the angle of the motor
    public virtual void setRotation(float angle)
    {
        //Debug.Log(this.gameObject.name + " attempting to reach " +  angle);

        spring.targetPosition = angle;
        joint.useMotor = false;
        joint.useSpring = true;
        joint.spring = spring;
    }

    //Set the speed of the motor
    public virtual void setSpeed(float speed) 
    {
        motor.targetVelocity = speed;
        joint.useSpring = false;
        joint.useMotor = true;
        joint.motor = motor;
    }

    public virtual float getEncoder()
    {
        //Debug.Log(joint.angle);
        //return joint.angle;
        float encoderAngle = float.NaN;
        switch (encoderAxis)
        {
            case EncoderAxis.X_AXIS: 
                encoderAngle = this.gameObject.transform.localEulerAngles.x; 
                break;
            case EncoderAxis.Y_AXIS: 
                encoderAngle = this.gameObject.transform.localEulerAngles.y; 
                break;
            case EncoderAxis.Z_AXIS: 
                encoderAngle = this.gameObject.transform.localEulerAngles.z;
                break;
        }

        return encoderAngle;
    }

}
