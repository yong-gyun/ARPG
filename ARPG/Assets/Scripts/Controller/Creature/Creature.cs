using Common.State;
using Data.Contents;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class Creature : InitBase
{
    public IObservable<IState> OnChangeStateEvent { get { return _onChangeStateEvent.AsObservable(); } }
    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }
    public CreatureInfo Info { get; set; }
    protected Vector3 _dir;

    protected Dictionary<Enum, IState> _states = new Dictionary<Enum, IState>();
    protected Subject<IState> _onChangeStateEvent = new Subject<IState>();
    protected IState _currentState;

    protected ColliderEventCallback _colliderEventCallback;
    protected BaseStats _stats;
    protected Animator _anim;

    [SerializeField] protected GameObject _model;

    protected void AddState(Enum stateKey, IState state)
    {
        _states.Add(stateKey, state);
        state.Init(this);
    }

    public void ChangeState(Enum key)
    {
        if (_states.TryGetValue(key, out var state) == false)
            return;

        if (_currentState != null)
            _currentState.Exit();

        _currentState = state;
        state.Enter();

        _onChangeStateEvent.OnNext(state);
    }

    public void SetAnimation(string animationName, float duration = 0.2f, int layer = 0)
    {
        _anim.CrossFade(animationName, duration, layer);
    }

    public async virtual void SetInfo(int templateID)
    {
        Define.CreatureType creatureType = templateID.GetCreatureType();

        Info = Managers.Data.GetCreatureInfoDatas.Find(info => info.TemplateID == templateID);
        _model = await Managers.Resource.InstantiateAsync($"Creature/{creatureType}", $"{Info.PrefabName}/{Info.PrefabName}.prefab", transform);
        _anim = _model.GetComponent<Animator>();
        _stats = gameObject.GetOrAddComponent<BaseStats>();
        _stats.Init(templateID);

        _colliderEventCallback = _model.GetOrAddComponent<ColliderEventCallback>();
    }
}