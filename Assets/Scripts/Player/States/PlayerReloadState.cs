using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerBaseState
{
    public override void Enter()
    {
        player.isReloading = true;
        player.InvokeStartReloading();
        player.StartRestoreConstantHealth();
    }

    public override void Perform()
    {
        Color floorColor = player.PaintManager.GetColorOfFloor(player.transform.position);

        if (floorColor != player.PlayerColor)
        {
            if (floorColor != Color.black)
            {
                stateMachine.ChangeState(new DamageState());
            }
            else
            {
                stateMachine.ChangeState(new PlayerNothingState());
            }
        }
    }
    public override void Exit()
    {
        player.isReloading = false;
        player.InvokeStopReloading();
        player.StopRestoreConstantHealth();
    }
}
