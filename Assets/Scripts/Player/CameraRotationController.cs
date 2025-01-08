using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    //[SerializeField] Transform orientation;
    //[SerializeField] Transform playableCharacterTransform;
    Transform playerTransform;
    //[SerializeField] Rigidbody playableCharacterRigidbody;
    //[SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 viewDir = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
            playerTransform.forward = viewDir;
        }
        //orientation.forward = viewDir.normalized;

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //Vector3 inputDir = orientation.forward * Mathf.Abs(verticalInput) + orientation.right * horizontalInput;

        //if (inputDir != Vector3.zero)
        //    playerTransform.forward = Vector3.Slerp(playerTransform.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }
}
