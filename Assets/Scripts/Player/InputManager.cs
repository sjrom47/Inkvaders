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
        
    }
    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerController != null)
        {
            horizontalStraightMovementInput = Input.GetAxis("Vertical");
            horizontalSideMovementInput = Input.GetAxis("Horizontal");
            playerController.ApplyPlayerMovementAndShot(horizontalStraightMovementInput, horizontalSideMovementInput, isShooting);
        }
    }

    private void Update()
    {
        if (playerController != null)
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
}
