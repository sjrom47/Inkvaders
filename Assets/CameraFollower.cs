using UnityEngine;

public class CameraFollower : MonoBehaviour
{   
    // Target de la c�mara
    [SerializeField]
    public Transform target;
       
    // Velocidad de la c�mara
    public float smoothSpeed = 0.125f;

    // Vector offset de la c�mara
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;

        transform.LookAt(target);
    }


    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;


    }
}
