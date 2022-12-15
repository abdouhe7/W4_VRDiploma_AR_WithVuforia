using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vr
{


public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.right * variableJoystick.Horizontal + Vector3.forward * variableJoystick.Vertical;
            transform.position += direction * speed * Time.deltaTime;
    }
}
}