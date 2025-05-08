using System;
using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System.Linq;

public class BaseEventEffect : BaseEffect
{
    public enum EventType
    {
        ColliderTrigger,
        ColliderStay,
        ColliderExit,
    }

    [SerializeField] protected EventType _eventType;
    [SerializeField] protected GameObject _eventObject;
    [SerializeReference, SubclassSelector] protected BaseEffect _event;

    protected ColliderEventHandler _eventHandler;

    public override void Init(Creature owner)
    {
        base.Init(owner);
        _eventHandler = _eventObject.GetOrAddComponent<ColliderEventHandler>();
    }

    public override void PlayAction()
    {
        
    }
}

[Serializable]
public class DamageEventEffect : BaseEventEffect
{
    public int skillID;
    public int skillArg;

    [SerializeField] protected List<Define.CreatureType> _targetCreatures;

    public override void Init(Creature owner)
    {
        base.Init(owner);

        if (_eventType == EventType.ColliderTrigger)
        {
            _eventHandler.OnTriggerEnterCallback.Subscribe(other =>
            {
                Creature creature = other.GetComponent<Creature>();
                if (creature == null)
                    return;

                if (_targetCreatures.Any(x => x == creature.CreatureType) == true)
                {
                    var skillScript = Managers.Data.SkillInfoDict[skillID].Find(x => x.SkillArg == skillArg);
                    creature.TakeDamage(skillScript, owner);
                }
            });
        }
        else if (_eventType == EventType.ColliderExit)
        {
            _eventHandler.OnTriggerExitCallback.Subscribe(other =>
            {
                Creature creature = other.GetComponent<Creature>();
                if (creature == null)
                    return;

                if (_targetCreatures.Any(x => x == creature.CreatureType) == true)
                {
                    var skillScript = Managers.Data.SkillInfoDict[skillID].Find(x => x.SkillArg == skillArg);
                    creature.TakeDamage(skillScript, owner);
                }
            });
        }
    }
}

[Serializable]
public class DotDamageEventEffect : DamageEventEffect
{
    public override void Init(Creature owner)
    {
        Owner = owner;

        
    }
}

