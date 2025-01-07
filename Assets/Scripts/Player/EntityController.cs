using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : BaseController
{
    
    //float verticalMovementInput;

    
    void Awake()
    {
        base.Awake();
    }

    public void ApplyMovement(float horizontalStraightMovementInput, float horizontalSideMovementInput)
    {
        if (horizontalStraightMovementInput < 0)
        {
            Debug.Log("Atras");
            moveBackwardCommand.Execute();
        }
        else if (horizontalStraightMovementInput > 0)
        {
            Debug.Log("Delante");
            moveForwardCommand.Execute();
        }

        else if (horizontalSideMovementInput < 0)
        {
            turnLeftCommand.Execute();
        }
        else if (horizontalSideMovementInput > 0)
        {
            turnRightCommand.Execute();
        }

        //if (verticalMovementInput > 0)
        //{
        //    jumpCommand.Execute();
        //}
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
