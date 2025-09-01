using System;
using System.Collections.Generic;
using UnityEngine;

public class EventActionRunner
{
    private OnEventAction[] _actions = new OnEventAction[(int)Define.EventActionType.Max];

    public delegate void OnEventAction(EventAction evt);

    public void AddEventAction(Define.EventActionType eventType, OnEventAction eventAction)
    {
        if (eventType == Define.EventActionType.None)
            return;

        _actions[(int)eventType] += eventAction;
    }

    public OnEventAction GetAction(Define.EventActionType type)
    {
        return _actions[(int)type];
    }

    public void Action(EventAction evtAction)
    {
        _actions[(int)evtAction.Type].Invoke(evtAction);
    }

    public void Clear()
    {
        for (int i = 0; i < _actions.Length; i++)
        {
            var item = _actions[i];
            _actions[i] = null;
        }
    }
}
