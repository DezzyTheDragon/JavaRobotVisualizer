using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a base class to allow users to build their own motor controller system
//  using a system of their choosing. The reason this is an abstract class as
//  opposed to an interface is because the Unity Editor will not display a
//  List<Interface> for the user to edit. Using a base class allows for the user
//  to add or remove motor controllers from lists in the editor.
public abstract class BaseMotorController : MonoBehaviour
{
    public abstract void setRotation(float angle);
    public abstract void setSpeed(float speed);
    public abstract float getEncoder();
}
