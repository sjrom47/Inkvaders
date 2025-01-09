using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour, IStateMachine
{
    public BaseEnemyState activeState;
    public void Initialize()
    {
        ChangeState(new PatrolState());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null) // clean activeState if any
        {
            activeState.Exit();
        }

        activeState = (BaseEnemyState)newState; // change to new state

        if (activeState != null)
        {
            activeState.stateMachine = this; // set up new state
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter(); // assign state
        }
    }
}
