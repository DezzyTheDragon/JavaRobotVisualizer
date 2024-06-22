using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMotorController
{
    public abstract void setRotation(float angle);
    public abstract void setSpeed(float speed);
    public abstract float getEncoder();
}
