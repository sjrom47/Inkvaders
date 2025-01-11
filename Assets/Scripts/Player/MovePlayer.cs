using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This class handles all methods that moves the player it's attached to
public class MovePlayer : MonoBehaviour
{
    //Speed of the object
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float drag = 0.5f;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = drag;
    }

    //These methods will be executed by their own command
    public void MoveForward()
    {
        Move(transform.forward);
    }

    public void MoveBackward()
    {
        Move(-transform.forward);
    }

    public void TurnLeft()
    {
        Move(-transform.right);
    }

    public void TurnRight()
    {
        Move(transform.right);
    }
    //public void Jump()
    //{
    //    Move(Vector3.up);
    //}

    //Help method to make it more general
    private void Move(Vector3 dir)
    {
        rb.AddForce(dir.normalized * moveSpeed * 10f, ForceMode.Force);
        
    }
}
