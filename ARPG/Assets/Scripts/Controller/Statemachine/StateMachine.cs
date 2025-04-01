using UnityEngine;
using System.Collections.Generic;

public class StateMachine<CreatureType>
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


}
