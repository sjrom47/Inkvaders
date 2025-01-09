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
        Debug.Log("En nothing state");
        Debug.Log(floorColor == player.PlayerColor);
        if (player.IsSquid && floorColor == player.PlayerColor)
        {
               stateMachine.ChangeState(new PlayerReloadState());
        }
        else
        {
            if (floorColor != player.PlayerColor && floorColor != Color.black)
            {
                stateMachine.ChangeState(new DamageState());
            }
        }
    }
    public override void Exit()
    {

    }
}
