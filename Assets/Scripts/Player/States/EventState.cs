using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventState : BaseState
{
    private UnityEvent onEnterEvent;
    private UnityEvent onExitEvent;

    public EventState(UnityEvent onEnterEvent, UnityEvent onExitEvent)
    {
        this.onEnterEvent = onEnterEvent;
        this.onExitEvent = onExitEvent;
    }

    public override void Enter()
    {
        onEnterEvent?.Invoke();
    }
    public override void Perform()
    {

    }
    public override void Exit()
    {
        onExitEvent?.Invoke();
    }
}