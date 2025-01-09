using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    public void Initialize() {}
    public void ChangeState(BaseState newState) {}
}
