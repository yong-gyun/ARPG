using System.Collections.Generic;
using UnityEngine;

public class EventActionBehaviour
{
    public float Elapsed { get { return _elapsed; } }

    private float _elapsed;

    //runner, action들을 실제로 실행시키는 역할
    public EventActionRunner Runner { get { return _runner; } }
    
    public List<EventAction> events = new List<EventAction>();
    
    private EventActionRunner _runner;

    public void OnUpdate(float elapsed, EventActionRunner runner)
    {
        foreach (var evt in events)
        {
            if (evt.Time >= elapsed)
                continue;

            _runner.Action(evt);
        }
    }
 }