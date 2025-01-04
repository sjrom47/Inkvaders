using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwardCommand : Command
{
    private MovePlayer movePlayer;

    public MoveBackwardCommand(MovePlayer movePlayer)
    {
        this.movePlayer = movePlayer;
    }

    public override void Execute()
    {
        movePlayer.MoveBackward();
    }

    //Undo is just the opposite
    public override void Undo()
    {
        movePlayer.MoveForward();
    }
}
