using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementcopy : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    [SerializeField] Vector3 PositionOffset;
    [SerializeField] Vector3 RotationOffset;
    Quaternion rotationOffsetQuat;
    void Start()
    {
         rotationOffsetQuat = Quaternion.Euler(RotationOffset);
    }

    // Update is called once per frame
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
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

