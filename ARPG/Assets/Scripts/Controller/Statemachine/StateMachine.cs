using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class StateMachine<CreatureType>
{
    class StateData
    {
        public State<CreatureType> State { get; private set; }
        public int Priority { get; private set; }
        public List<StateTransition<CreatureType>> Transitions { get; private set; } = new();
        
        public StateData(State<CreatureType> state, int priority)
        {
            State = state; 
            Priority = priority; 
        }
    }

    private Dictionary<Type, StateData> _stateDatas = new Dictionary<Type, StateData>();
    private List<StateTransition<CreatureType>> _anyTransitions = new List<StateTransition<CreatureType>>();
    private StateData _currentStateData;
    
    public CreatureType Owner { get; private set; }

    //������Ʈ �ӽ�, ������ ������Ʈ, ���� ������Ʈ, ������Ʈ���� ���� ���̾�
    public Action<StateMachine<CreatureType>, State<CreatureType>, State<CreatureType>> OnStateChangeHandler;

    public void Init(CreatureType owner)
    {
        Owner = owner;
        AddStates();
        MakeTransitions();

        foreach (var item in _stateDatas)
        {
            _currentStateData = null;

            //�켱������ ���� ���� ������Ʈ ���� �� ����
            var stateData = _stateDatas.Values.First(x => x.Priority == 0);
            ChangeState(stateData);
        }
    }

    private void ChangeState(StateData stateData)
    {
        var prevState = _currentStateData;
        prevState?.State.Exit();

        _currentStateData = stateData;
        stateData.State.Enter();

        OnStateChangeHandler?.Invoke(this, stateData.State, prevState.State);
    }

    private void ChangeState(State<CreatureType> state)
    {
        var stateData = _stateDatas[state.GetType()];
        ChangeState(stateData);
    }

    private bool TryTransition(IReadOnlyList<StateTransition<CreatureType>> transitions)
    {
        foreach (var transition in transitions)
        {
            //���� ������ �������� ���ϸ� �� �Ѿ
            if (transition.IsTransferable == false)
                continue;

            //CanTarnsitionToSelf�� false�� �����ؾ��� ToState�� CurrentState�� ���Ƹ� �Ѿ
            if (transition.CanTransitionChangeToSelf == false && _currentStateData.State == transition.ToState)
                continue;

            ChangeState(transition.ToState);
            return true;
        }

        return false;
    }

    public void AddState<T>() where T : State<CreatureType>
    {
        var newState = Activator.CreateInstance<T>();
        newState.Init(this, Owner);

        bool condition = _stateDatas.ContainsKey(typeof(T)) == false;
        Debug.Assert(condition, $"StateMachine::AddState<{typeof(T).Name}> - �̹� ���°� �����մϴ�.");

        var stateData = new StateData(newState, _stateDatas.Count);
        _stateDatas.Add(typeof(T), stateData);
    }

    public void MakeTransition<FromStateType, ToStateType>(Func<State<CreatureType>, bool> transitionCondition, int layer = 0) where FromStateType : State<CreatureType> where ToStateType : State<CreatureType>
    {
        var fromStateData = _stateDatas[typeof(FromStateType)];
        var toStateData = _stateDatas[typeof(ToStateType)];

        var newTransition = new StateTransition<CreatureType>(fromStateData.State, toStateData.State, transitionCondition, true);
        fromStateData.Transitions.Add(newTransition);
    }

    public void MakeAnyTransition<ToStateType>(int transitionCommand, Func<State<CreatureType>, bool> transitionCondition, int layer = 0, bool canTransitonToSelf = false) where ToStateType : State<CreatureType>
    {
        var state = _stateDatas[typeof(ToStateType)].State;
        var newTransition = new StateTransition<CreatureType>(null, state, transitionCondition, canTransitonToSelf);
        _anyTransitions.Add(newTransition);
    }

    public bool IsInState<T>() where T : State<CreatureType>
    {
        var stateType = _currentStateData.State.GetType();
        if (stateType == typeof(T))
            return true;

        return false;
    }

    public State<CreatureType> GetCurrentState()
    {
        return _currentStateData.State;
    }

    public Type GetCurrentStateType()
    {
        return GetCurrentState().GetType();
    }

    protected abstract void AddStates();    
    protected abstract void MakeTransitions();
}