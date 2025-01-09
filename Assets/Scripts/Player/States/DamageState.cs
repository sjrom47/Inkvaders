using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : PlayerBaseState
{

    public override void Enter()
    {
        player.StartTakeConstantDamage();
    }

    public override void Perform()
    {
        Color floorColor = player.PaintManager.GetColorOfFloor(player.transform.position);

        if (player.IsSquid && floorColor == player.PlayerColor)
        {
            stateMachine.ChangeState(new PlayerReloadState());
        }
        else
        {
            if (floorColor == player.PlayerColor || floorColor == Color.black)
            {
                stateMachine.ChangeState(new PlayerNothingState());
            }
        }
    }
    public override void Exit()
    {
        player.StopTakeConstantDamage();
    }
}
