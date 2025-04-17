using UnityEngine;
using Common.State;

namespace Creatures.HunterState
{
    public class IdleState : IState
    {
        private Hunter _owner;
        public Creature GetOwner() { return _owner; }
        public Vector3 GetDir() { return _owner.Dir; }
        public void Init(Creature owner)
        {
            _owner = owner.GetComponent<Hunter>();
        }

        public void Enter()
        {
            _owner.SetAnimation("Idle");
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_owner.Dir != Vector3.zero)
                _owner.State = Hunter.ESTATE.MOVE;
        }
    }
}
