using System;
using UnityEngine;

[Serializable]
public class PositionEventAction : BaseEventAction
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private float _speed;

    public override void OnEnter()
    {

    }

    public override void OnUpdate(float deltaTime)
    {

    }
}