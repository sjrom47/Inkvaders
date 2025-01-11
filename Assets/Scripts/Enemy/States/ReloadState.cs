using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : BaseEnemyState
{
    private float moveTimer;
    private float losePlayerTimer;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.path.waypoints[0].position);
        enemy.PlayerController.AnimController.Animate(Direction.FORWARD, false);
        enemy.PlayerController.WeaponHolder.TryStopShoot();
    }
    public override void Perform()
    {
        if (enemy.PlayerController.WeaponHolder.GetCurrentWeapon().Amunition() >= 50) 
        {

            if (enemy.CanSeePlayer())
            {
                stateMachine.ChangeState(new AtackState());
            }
            else
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    public override void Exit()
    {

    }
}
