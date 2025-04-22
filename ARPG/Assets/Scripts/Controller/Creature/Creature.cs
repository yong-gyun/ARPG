using Cysharp.Threading.Tasks;
using Data.Contents;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public Define.CreatureState State { get { return _state; } }

    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }
    public CreatureInfoScript Info { get; set; }
    public bool IsInitialized { get { return _init; } }
    
    protected Vector3 _dir;
    
    protected Animator _anim;

    protected bool _init;

    [SerializeField] protected GameObject _model;

    [SerializeField] protected Define.CreatureState _state;

    public void SetAnimation(string animationName, float duration = 0.1f, int layer = 0)
    {
        _anim.CrossFade(animationName, duration, layer);
    }

    public virtual async UniTask Init(int templateID)
    {
        if (_init == true)
            return;

        Define.CreatureType creatureType = templateID.GetCreatureType();
        Info = Managers.Data.GetCreatureInfoScripts.Find(info => info.TemplateID == templateID);
        
        _model = await Managers.Resource.InstantiateAsync($"Creature/{creatureType}", $"{Info.PrefabName}/{Info.PrefabName}.prefab", transform);
        _anim = _model.GetComponent<Animator>();
        _init = true;
    }

    protected virtual void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        switch (State)
        {
            case Define.CreatureState.IDLE:
                UpdateIdle();
                break;
            case Define.CreatureState.MOVE:
                UpdateMove();
                break;
            case Define.CreatureState.SKILL:
                UpdateSkill();
                break;
            case Define.CreatureState.HIT:
                UpdateHit();
                break;
            case Define.CreatureState.DEAD:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateHit() { }
    protected virtual void UpdateDead() { }

    public abstract void ChangeState(Define.CreatureState state);
}