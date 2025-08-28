using System.Collections.Generic;
using UnityEngine;

public class EventActionRunner
{
    public float Elapsed { get { return _elapsed; } }

    [SerializeField] private List<BaseEventAction> _eventActions = new List<BaseEventAction>();
    private BaseObject _owner;

    private float _elapsed = 0f;

    public void SetOwner(BaseObject owner)
    {
        owner = _owner;
    }
}
