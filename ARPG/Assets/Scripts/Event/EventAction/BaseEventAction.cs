using System;
using UnityEngine;

public class BaseEventAction
{
    public float ActionTime { get { return _actionTime; } }
    public float Length { get; }
    public Define.EventActionType EventActionType { get; set; }

    [SerializeField] protected float _actionTime;
    [SerializeField] protected float _lenght;
    protected BaseObject _owner;


    public void SetOwner(BaseObject owner)
    {
        _owner = owner;
    }

    public virtual void OnEnter() { }
    public virtual void OnUpdate(float deltaTime) { }
    public virtual void OnExit() { }
}