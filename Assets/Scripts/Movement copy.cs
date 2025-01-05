using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementcopy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
    [SerializeField] Vector3 PositionOffset;
    [SerializeField] Vector3 RotationOffset;
    Quaternion rotationOffsetQuat;
    void Start()
    {
         rotationOffsetQuat = Quaternion.Euler(RotationOffset);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            // Copy the final position and rotation after animations
            //transform.position = target.position + PositionOffset;
            transform.position = target.TransformPoint(PositionOffset);
            transform.rotation = target.rotation * rotationOffsetQuat;
        }
    }
}

