using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalStraightMovementInput;
    public float horizontalSideMovementInput;
    public float verticalMovementInput;

    public MovePlayer movePlayer;
    public MoveForwardCommand moveForwardCommand;
    public MoveBackwardCommand moveBackwardCommand;
    public TurnRightCommand turnRightCommand;
    public TurnLeftCommand turnLeftCommand;
    public JumpCommand jumpCommand;

    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        moveForwardCommand = new MoveForwardCommand(movePlayer);
        moveBackwardCommand = new MoveBackwardCommand(movePlayer);
        turnRightCommand = new TurnRightCommand(movePlayer);
        turnLeftCommand = new TurnLeftCommand(movePlayer);
        jumpCommand = new JumpCommand(movePlayer);

        GeneralResetSquidTransformation();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        ApplyMovement();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GeneralFlipSquidTransormation();
        }
        horizontalStraightMovementInput = Input.GetAxis("Vertical");
        horizontalSideMovementInput = Input.GetAxis("Horizontal");
        verticalMovementInput = Input.GetAxis("Jump");
    }

    void ApplyMovement()
    {
        if (horizontalStraightMovementInput < 0)
        {
            Debug.Log("Atras");
            moveBackwardCommand.Execute(); 
        }
        else if (horizontalStraightMovementInput > 0)
        {
            Debug.Log("Delante");
            moveForwardCommand.Execute(); 
        }

        if (horizontalSideMovementInput < 0) 
        { 
            turnLeftCommand.Execute(); 
        }
        else if (horizontalSideMovementInput > 0) 
        { 
            turnRightCommand.Execute(); 
        }

        if (verticalMovementInput > 0)
        {
            jumpCommand.Execute();
        }
    }

    // Reset the logic of the Player state
    void GeneralResetSquidTransformation()
    {
        ResetSizeSquidTrasformation();
    }

    // Flip the logic between Player and Squid state
    void GeneralFlipSquidTransormation()
    {
        FlipSizeSquidTrasformation();
    }

    // Reset the size to the one of the Player state
    void ResetSizeSquidTrasformation()
    {
        transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled = true;
        transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled = false;
    }

    // Flip the size between Player and Squid state
    void FlipSizeSquidTrasformation()
    {
        transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled = !transform.Find("NormalSize").GetComponent<MeshRenderer>().enabled;
        transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled = !transform.Find("SmallSize").GetComponent<MeshRenderer>().enabled;
    }
}
