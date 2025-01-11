using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerNothingState : PlayerBaseState
{
    public override void Enter()
    {

    }

    public override void Perform()
    {
        Color floorColor = player.PaintManager.GetColorOfFloor(player.transform.position);

        if (player.IsSquid && ColorChecker.ColorsAreClose(floorColor, player.PlayerColor))
        {
            stateMachine.ChangeState(new PlayerReloadState());
        }
        else
        {
            if (!ColorChecker.ColorsAreClose(floorColor, player.PlayerColor) && !ColorChecker.ColorsAreClose(floorColor, Color.black))
            {
                stateMachine.ChangeState(new DamageState());
            }
        }
    }
    public override void Exit()
    {

    }
}
