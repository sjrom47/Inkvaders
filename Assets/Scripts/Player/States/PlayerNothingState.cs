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
        //Debug.Log("En nothing State");
        //Debug.Log(player.IsSquid);
        //Debug.Log(ColorChecker.ColorsAreClose(floorColor, player.PlayerColor));
        //Debug.Log(player.PlayerColor);
        //Debug.Log(floorColor);
        //Debug.Log(player.IsSquid && ColorChecker.ColorsAreClose(floorColor, player.PlayerColor));
        //Debug.Log(stateMachine);
        if (player.IsSquid && ColorChecker.ColorsAreClose(floorColor, player.PlayerColor))
        {
            //Debug.Log("Vas a cambiar de estado");
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
