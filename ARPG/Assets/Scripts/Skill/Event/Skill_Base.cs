using System.Collections.Generic;
using UnityEngine;
using static Skill;

public class Skill_Base
{
    protected Creature _owner;

    [SerializeField] protected Define.TargetType _targetType;

    public ExecuteType executeType = ExecuteType.Colision;   //스킬 판정 (충돌, 즉시 발동 등)

    [SerializeField] protected int _skillArg;

    [SerializeField] protected List<Creature> _targets = new List<Creature>();

    protected int _skillID;

    public virtual void Init(Creature creature, int skillID)
    {
        _owner = creature;
        _skillID = skillID;
    }

    public virtual void Execute() { }

    public virtual void Apply(Creature target) { }

    public virtual void Release(Creature target) { }
}