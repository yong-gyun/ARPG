using UniRx;
using Data.Contents;
using System.Collections.Generic;
using System.Collections.Generic.Serialized;
using UnityEngine;
using System.Linq;

[System.Serializable]
public abstract class EffectBase
{
    protected Effect _parent;

    public virtual void Init(Effect parent) 
    { 
        _parent = parent;
    }

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Destory() { }
}

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

public class EffectCollider : EffectBase
{
    [SerializeField] private ColliderEventHandler _eventHandler;

    [SerializeField] private SerializedDictionary<Define.ColliderEventType, EffectBase> _eventEffectList = new SerializedDictionary<Define.ColliderEventType, EffectBase>();        //충돌 후 발생할 이벤트 이펙트들

    public override void Start()
    {
        _eventHandler.OnTriggerEnterCallback.Subscribe(others =>
        {
            var targets = others.GetComponents<Creature>().ToList();
            foreach ((Define.ColliderEventType key, EffectBase value) mit in _eventEffectList)
            {
                if (mit.key == Define.ColliderEventType.Enter)
                {
                    if (mit.value is EffectSkillBase)
                    {
                        EffectSkillBase es = mit.value as EffectSkillBase;
                        es.SetTargets(targets, _parent.Owner);
                    }

                    mit.value.Start();
                }
            }
        });

        _eventHandler.OnTriggerStayCallback.Subscribe(others =>
        {
            var targets = others.GetComponents<Creature>().ToList();
            foreach ((Define.ColliderEventType key, EffectBase value) mit in _eventEffectList)
            {
                if (mit.key == Define.ColliderEventType.Stay)
                {
                    if (mit.value is EffectSkillBase)
                    {
                        EffectSkillBase es = mit.value as EffectSkillBase;
                        es.SetTargets(targets, _parent.Owner);
                    }

                    mit.value.Update();
                }
            }
        });

        _eventHandler.OnTriggerExitCallback.Subscribe(others =>
        {
            var targets = others.GetComponents<Creature>().ToList();
            foreach ((Define.ColliderEventType key, EffectBase value) mit in _eventEffectList)
            {
                if (mit.key == Define.ColliderEventType.Exit)
                {
                    if (mit.value is EffectSkillBase)
                    {
                        EffectSkillBase es = mit.value as EffectSkillBase;
                        es.SetTargets(targets, _parent.Owner);
                    }

                    mit.value.Destory();
                }
            }
        });
    }

    public override void Destory()
    {
        _eventHandler.Clear();
    }
}

public class EffectDamage : EffectSkillBase
{
    [SerializeField] private int _skillArg;

    public override void Start()
    {
        var script = Managers.Data.GetSkillArg(_parent.SkillID, _skillArg);
        foreach (var item in _targets)
            item.TakeDamage(script, _parent.Owner);
    }
}

public class EffectDelayActive : EffectBase
{
    [SerializeField] private float _delay;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private bool _isActive;

    private float _currentTime;
    private bool _completed;

    public override void Start()
    {
        _currentTime = 0f;
        _completed = false;
    }

    public override void Update()
    {
        if (_completed == true)
            return;

        _currentTime += Time.deltaTime;
        if (_currentTime >= _delay)
        {
            _targetObject.SetActive(_isActive);
            _completed = true;
        }
    }
}