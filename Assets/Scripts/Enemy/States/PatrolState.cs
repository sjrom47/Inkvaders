using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseEnemyState
{
    public int waypointindex;
    public float waitTimer;
    public override void Enter()
    {
        if (enemy.PlayerController != null)
        {
            enemy.PlayerController.WeaponHolder.TryStopShoot();
        }
    }
    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AtackState());
        }
        else if (enemy.PlayerController != null && enemy.PlayerController.WeaponHolder.GetCurrentWeapon().Amunition() <= 0)
        {
            stateMachine.ChangeState(new ReloadState());
        }
    }
    public override void Exit()
    {

    }
    private void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f && enemy.PlayerController != null)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 0.3)
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
                enemy.PlayerController.AnimController.Animate(Direction.FORWARD, false);
                waitTimer = 0;
            }
            else
            {
                enemy.PlayerController.AnimController.Animate(Direction.NONE, false);
            }
        }
    }
}
