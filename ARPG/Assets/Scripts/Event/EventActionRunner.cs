using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EventActionRunner
{
    private OnEventAction[] _actions = new OnEventAction[(int)Define.EventActionType.Max];

    public delegate void OnEventAction(EventAction evt);

    public void AddEventAction(Define.EventActionType eventType, OnEventAction eventAction)
    {
        if (eventType == Define.EventActionType.None)
            return;

        _actions[(int)eventType] = eventAction;
    }

    public OnEventAction GetAction(EventActionType type)
    {
        OnEventAction action = _actions[(int)type];

        return action;
    }

    public void Action(EventAction evtAction)
    {
        _actions[(int)evtAction.Type].Invoke(evtAction);
    }
}
