using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill_Base
{
    protected Creature _owner;

    [SerializeField] protected Define.TargetType _targetType;

    public Define.SkillExecuteType executeType = Define.SkillExecuteType.Colision;   //��ų ���� (�浹, ��� �ߵ� ��)

    [SerializeField] protected int _skillArg;

    [SerializeField] protected List<Creature> _targets = new List<Creature>();

    protected int _skillID;

    public virtual void Init(Creature creature, int skillID)
    {
        _owner = creature;
        _skillID = skillID;
    }

    public abstract void Execute();
    public abstract void Apply(Creature target);
    public abstract void Exit(Creature target);
    public abstract void Release();
}