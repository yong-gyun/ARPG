using Common.State;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class Creature : InitBase
{
    public IObservable<IState> OnChangeStateEvent { get { return _onChangeStateEvent.AsObservable(); } }
    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }
    protected Vector3 _dir;

    protected Dictionary<Enum, IState> _states = new Dictionary<Enum, IState>();
    protected Subject<IState> _onChangeStateEvent;
    protected IState _currentState;

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
        _currentState.Exit();

        _currentState = state;
        state.Enter();

        _onChangeStateEvent.OnNext(state);
    }

    public void SetAnimation(Enum animation, float normalizedTransitionDuration = 0.1f)
    {
        _anim.CrossFade(animation.ToString(), normalizedTransitionDuration);
    }

    public abstract void SetInfo(int creatureID);
}
