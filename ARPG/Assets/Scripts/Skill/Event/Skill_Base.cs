using System.Collections.Generic;
using UnityEngine;

public class Skill_Base
{
    protected Creature _owner;

    [SerializeField] protected Define.TargetType _targetType;

    [SerializeField] protected int _skillArg;
    protected int _skillID;

    public virtual void Init(Creature creature, int skillID)
    {
        _owner = creature;
        _skillID = skillID;
    }

    public virtual void OnEnter() { }

    public virtual void OnUpdate(float deltaTime) { }

    public virtual void OnExit() { }
}