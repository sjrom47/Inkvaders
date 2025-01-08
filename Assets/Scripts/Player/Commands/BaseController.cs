using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected float horizontalStraightMovementInput;
    protected float horizontalSideMovementInput;
    //float verticalMovementInput;

    protected MovePlayer movePlayer;
    protected MoveForwardCommand moveForwardCommand;
    protected MoveBackwardCommand moveBackwardCommand;
    protected TurnRightCommand turnRightCommand;
    protected TurnLeftCommand turnLeftCommand;
    // Start is called before the first frame update
    protected void Awake()
    {

        movePlayer = GetComponent<MovePlayer>();
        moveForwardCommand = new MoveForwardCommand(movePlayer);
        moveBackwardCommand = new MoveBackwardCommand(movePlayer);
        turnRightCommand = new TurnRightCommand(movePlayer);
        turnLeftCommand = new TurnLeftCommand(movePlayer);
    }
}
