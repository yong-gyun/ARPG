using System.Collections.Generic;
using UnityEngine;

public class EffectSkillBase : EffectBase
{
    protected Creature _attacker;
    protected List<Creature> _targets;

    public virtual void SetTargets(List<Creature> targets, Creature attacker)
    {
        _attacker = attacker;
        _targets = targets;
    }
}