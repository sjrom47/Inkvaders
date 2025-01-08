using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointindex;
    public float waitTimer;
    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AtackState());
        }
    }
    public override void Exit()
    {

    }
    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                if (waypointindex < enemy.path.waypoints.Count - 1)
                {
                    waypointindex++;
                }
                else
                {
                    waypointindex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointindex].position);
                //enemy.PlayerController.AnimController.Animate(Direction.FORWARD, false);
                //enemy.PlayerController.WeaponHolder.TryStopShoot();
                waitTimer = 0;
            }
            else
            {
                enemy.PlayerController.AnimController.Animate(Direction.NONE, true);
                enemy.PlayerController.WeaponHolder.TryShoot();
            }
        }
    }
}
