using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] Transform playableCharacterTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody playableCharacterRigidbody;
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = playableCharacterTransform.position - new Vector3(transform.position.x, playableCharacterTransform.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * Mathf.Abs(verticalInput) + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            playerTransform.forward = Vector3.Slerp(playerTransform.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }
}
