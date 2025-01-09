using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IStateMachine
{
    public PlayerBaseState activeState;
    public void Initialize()
    {
        Debug.Log("Se está inicializando");
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

    public void ChangeState(BaseState newState)
    {
        Debug.Log("Has en trado en cambiar de estado");
        if (activeState != null) // clean activeState if any
        {
            activeState.Exit();
            Debug.Log(activeState.ToString());
        }
        //Debug.Log(activeState.ToString());
        Debug.Log(newState.ToString());
        activeState = (PlayerBaseState)newState; // change to new state

        if (activeState != null)
        {
            activeState.stateMachine = this; // set up new state
            Debug.Log(activeState.stateMachine.ToString());
            activeState.player = GetComponent<Player>();
            activeState.Enter(); // assign state
        }
    }
}
