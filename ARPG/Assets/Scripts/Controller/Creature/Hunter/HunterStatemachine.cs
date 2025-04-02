using Creatures.HunterState;
using UnityEngine;

namespace Creatures.Statemachine
{
    public class HunterStatemachine : StateMachine<Hunter>
    {
        protected override void AddStates()
        {
            AddState<IdleState>();
            AddState<MoveState>();
            AddState<DashState>();
            AddState<AttackState>();
            AddState<SkillState>();
            AddState<HitState>();
            AddState<DeadState>();
        }

        protected override void MakeTransitions()
        {

        }
    }
}