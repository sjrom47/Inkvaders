using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    float horizontalStraightMovementInput;
    float horizontalSideMovementInput;
    //float verticalMovementInput;

    MovePlayer movePlayer;
    MoveForwardCommand moveForwardCommand;
    MoveBackwardCommand moveBackwardCommand;
    TurnRightCommand turnRightCommand;
    TurnLeftCommand turnLeftCommand;
    // Start is called before the first frame update
    protected void Awake()
    {
        
        movePlayer = GetComponent<MovePlayer>();
        moveForwardCommand = new MoveForwardCommand(movePlayer);
        moveBackwardCommand = new MoveBackwardCommand(movePlayer);
        turnRightCommand = new TurnRightCommand(movePlayer);
        turnLeftCommand = new TurnLeftCommand(movePlayer);
    }

    public void ApplyMovement(float horizontalStraightMovementInput, float horizontalSideMovementInput)
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

        else if (horizontalSideMovementInput < 0)
        {
            turnLeftCommand.Execute();
        }
        else if (horizontalSideMovementInput > 0)
        {
            turnRightCommand.Execute();
        }

        //if (verticalMovementInput > 0)
        //{
        //    jumpCommand.Execute();
        //}
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
