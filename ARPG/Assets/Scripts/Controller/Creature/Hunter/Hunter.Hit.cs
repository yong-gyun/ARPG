using Common.State;
using UnityEngine;

namespace Creatures.HunterState
{
    public class HitState : IState
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
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
