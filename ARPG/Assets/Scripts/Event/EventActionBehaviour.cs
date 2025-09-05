using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventActionBehaviour
{
    public float Elapsed { get { return _elapsed; } }

    private float _elapsed;

    
    public List<EventAction> events = new List<EventAction>();

    public void OnUpdate(float elapsed, EventActionRunner runner)
    {
        foreach (var evt in events)
        {
            if (evt.Time >= elapsed)
                continue;

            runner.Action(evt);
        }
    }
 }