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
            enemy.Agent.SetDestination(enemy.lastSeenEnemyPlayer.transform.position);
            enemy.PlayerController.AnimController.Animate(Direction.FORWARD, true);
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 5)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    public override void Exit()
    {

    }
}
