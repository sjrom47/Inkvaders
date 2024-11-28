using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command
{
    private MovePlayer movePlayer;
    public JumpCommand(MovePlayer movePlayer)
    {
        this.movePlayer = movePlayer;
    }

    public override void Execute()
    {
        movePlayer.MoveForward();
    }


    //Undo is just the opposite
    public override void Undo()
    {
        movePlayer.MoveBackward();
    }
}
