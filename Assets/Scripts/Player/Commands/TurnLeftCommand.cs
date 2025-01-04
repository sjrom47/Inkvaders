using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeftCommand : Command
{
    private MovePlayer movePlayer;

    public TurnLeftCommand(MovePlayer movePlayer)
    {
        this.movePlayer = movePlayer;
    }

    public override void Execute()
    {
        movePlayer.TurnLeft();
    }

    //Undo is just the opposite
    public override void Undo()
    {
        movePlayer.TurnRight();
    }
}
