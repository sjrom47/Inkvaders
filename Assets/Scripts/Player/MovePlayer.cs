using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles all methods that moves the player it's attached to
public class MovePlayer : MonoBehaviour
{
    //Speed of the object
    private const float MOVE_STEP_DISTANCE = 0.01f;

    //These methods will be executed by their own command
    public void MoveForward()
    {
        Move(Vector3.forward);
    }

    public void MoveBackward()
    {
        Move(Vector3.back);
    }

    public void TurnLeft()
    {
        Move(Vector3.left);
    }

    public void TurnRight()
    {
        Move(Vector3.right);
    }
    public void Jump()
    {
        Move(Vector3.up);
    }

    //Help method to make it more general
    private void Move(Vector3 dir)
    {
        transform.Translate(dir * MOVE_STEP_DISTANCE);
    }
}
