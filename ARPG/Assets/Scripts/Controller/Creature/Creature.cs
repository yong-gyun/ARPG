using Cysharp.Threading.Tasks;
using Data.Contents;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract partial class Creature : MonoBehaviour
{
    public Define.CreatureState State { get { return _state; } }

    public Define.CreatureType CreatureType { get; set; }

    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }
    
    public CreatureInfoScript Info { get; private set; }

    public bool IsInitialized { get { return _init; } }
    
    protected Vector3 _dir;
    
    protected Animator _anim;

    protected bool _init;

    [SerializeField] protected GameObject _model;

    [SerializeField] protected Define.CreatureState _state;

    [SerializeField] protected SkillEventHandler _skillEvent;

    protected ColliderEventHandler _colliderEvent;

    public void SetAnimation(string animationName, float duration = 0.1f, int layer = 0)
    {
        _anim.CrossFade(animationName, duration, layer);
        Debug.Log($"Set animation {animationName}");
    }

    public virtual async UniTask Init(int templateID)
    {
        if (_init == true)
            return;

        Define.CreatureType creatureType = templateID.GetCreatureType();
        Info = Managers.Data.GetCreatureInfoScripts.Find(info => info.TemplateID == templateID);
        
        _model = await Managers.Resource.InstantiateAsync($"Creature/{creatureType}/{Info.PrefabName}", $"{Info.PrefabName}.prefab", transform);
        
        _anim = _model.GetComponent<Animator>();
        _skillEvent = _model.GetOrAddComponent<SkillEventHandler>();
        _skillEvent.Init(this);

        SetStat(templateID);
        _init = true;
    }

    protected virtual void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    protected virtual void OnUpdate(float deltaTime)
    {
        switch (State)
        {
            case Define.CreatureState.Idle:
                UpdateIdle(deltaTime);
                break;
            case Define.CreatureState.Move:
                UpdateMove(deltaTime);
                break;
            case Define.CreatureState.Skill:
                UpdateSkill(deltaTime);
                break;
            case Define.CreatureState.Hit:
                UpdateHit(deltaTime);
                break;
            case Define.CreatureState.Dead:
                UpdateDead(deltaTime);
                break;
        }
    }

    protected virtual void UpdateIdle(float deltaTime) { }
    protected virtual void UpdateMove(float deltaTime) { }
    protected virtual void UpdateSkill(float deltaTime) { }
    protected virtual void UpdateHit(float deltaTime) { }
    protected virtual void UpdateDead(float deltaTime) { }

    public abstract void ChangeState(Define.CreatureState state);

    public virtual void TakeDamage(SkillLeveInfoScript script, Creature attacker)
    {
        var damageInfo = ExtendedHelper.CaculateDamage(script, this, attacker);

        _hp -= damageInfo.damage;

        if (_hp <= 0f)
        {
            ChangeState(Define.CreatureState.Dead);
        }
        else
        {
            ChangeState(Define.CreatureState.Hit);
        }
    }
}