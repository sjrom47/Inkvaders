using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackState : BaseEnemyState
{
    private float moveTimer;
    private float losePlayerTimer;

    public override void Enter()
    {

    }
    public override void Perform()
    {
        if (enemy.lastSeenEnemyPlayer.IsDead)
        {
            stateMachine.ChangeState(new PatrolState());
        }
        else if (enemy.CanSeePlayer()) 
        {
            losePlayerTimer = 0;
            enemy.transform.LookAt(enemy.lastSeenEnemyPlayer.transform.position);
            enemy.Agent.SetDestination(enemy.transform.position);
            enemy.PlayerController.AnimController.Animate(Direction.NONE, true);
            enemy.PlayerController.WeaponHolder.TryShoot();
        }
        else if (enemy.PlayerController.WeaponHolder.GetCurrentWeapon().Amunition() <= 0)
        {
            stateMachine.ChangeState(new ReloadState());
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 5)
            {
                stateMachine.ChangeState(new PatrolState());
            }
            else 
            {
                enemy.PlayerController.AnimController.Animate(Direction.NONE, false);
                enemy.PlayerController.WeaponHolder.TryStopShoot();
            }
        }
    }
    public override void Exit()
    {

    }
}
