using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;

    public override void Enter()
    {

    }
    public override void Perform()
    {
        if (enemy.CanSeePlayer()) 
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                enemy.PlayerController.animController.Animate(Direction.FORWARD, true);
                moveTimer = 0;
            }
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
