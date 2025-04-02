using System;
using UnityEngine;

public class StateTransition<T>
{
    public bool CanTransitionChangeToSelf { get; private set; }
    public State<T> FromState { get; private set; }
    public State<T> ToState { get; private set; }

    Func<State<T>, bool> _transitionCondition;

    public bool IsTransferable
    {
        get { return _transitionCondition == null || _transitionCondition.Invoke(FromState) == true; }
    }

    public StateTransition(State<T> fromState, State<T> toState, Func<State<T>, bool> transitionCondition, bool canTransitionChangeToSelf)
    {
        FromState = fromState;
        ToState = toState;
        _transitionCondition = transitionCondition;
        CanTransitionChangeToSelf = canTransitionChangeToSelf;
    }

}
