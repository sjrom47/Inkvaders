using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IStateMachine
{
    public PlayerBaseState activeState;
    public void Initialize()
    {
        ChangeState(new PlayerNothingState());
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

    public void ChangeState(PlayerBaseState newState)
    {
        if (activeState != null) // clean activeState if any
        {
            activeState.Exit();
        }

        activeState = newState; // change to new state

        if (activeState != null)
        {
            activeState.stateMachine = this; // set up new state
            activeState.player = GetComponent<Player>();
            activeState.Enter(); // assign state
        }
    }
}
