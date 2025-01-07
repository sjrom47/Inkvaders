using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    float horizontalStraightMovementInput;
    float horizontalSideMovementInput;
    bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        horizontalStraightMovementInput = Input.GetAxis("Vertical");
        horizontalSideMovementInput = Input.GetAxis("Horizontal");
        playerController.ApplyPlayerMovementAndShot(horizontalStraightMovementInput, horizontalSideMovementInput, isShooting);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerController.TransformIntoSquid();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            playerController.EndSquidTransformation();
        }
        if (!Input.GetKey(KeyCode.Q))
        {
             isShooting = Input.GetMouseButton(0);

        }
    }
}
