using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This allows you to controll multiple elements that are ment to be tied together from one point
//  For example a claw, each part has its own hinge joint, each moved and controlled as a single
//  object
public class MultiHingeJointMotorController : HingeJointMotorController
{
    [Header("Controlled Motors")]
    public List<HingeJoint> motorSet    = new List<HingeJoint>();   //List of motors that need to be controlled
    public List<bool>       invertMotor = new List<bool>();         //A list where it inverts the movement of the corresponding hinge joint

    void Start()
    {
        if(motorSet.Count != invertMotor.Count)
        {
            Debug.LogError("Motor list and invert list do not match!");
        }
        initJoint();
        setRotation(0);
    }

    //Iterate through all hinge joints and set a target angle
    public override void setRotation(float angle)
    {
        //Debug.Log("Setting multi-motor rotation");
        for(int i = 0; i < motorSet.Count; i++)
        {
            if (invertMotor[i])
            {
                spring.targetPosition = -angle;
            }
            else
            {
                spring.targetPosition = angle;
            }
            motorSet[i].spring = spring;
            motorSet[i].useMotor = false;
            motorSet[i].useSpring = true;
        }
    }

    //Iterate through all hinge joints and set a target speed
    public override void setSpeed(float speed)
    {
        for(int i = 0;i < motorSet.Count; i++)
        {
            if (invertMotor[i])
            {
                motor.targetVelocity = -speed;
            }
            else
            {
                motor.targetVelocity = speed;
            }
            motorSet[i].motor = motor;
            motorSet[i].useMotor = true;
            motorSet[i].useSpring = false;
        }
    }
}
