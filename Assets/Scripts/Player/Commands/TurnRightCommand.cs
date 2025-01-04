using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovePlayer;

public class TurnRightCommand : Command
{
    private MovePlayer movePlayer;

    public TurnRightCommand(MovePlayer movePlayer)
    {
        this.movePlayer = movePlayer;
    }

    public override void Execute()
    {
        movePlayer.TurnRight();
    }


    //Undo is just the opposite
    public override void Undo()
    {
        movePlayer.TurnLeft();
    }
}
