using Common.State;
using UnityEngine;

namespace Creatures.HunterState
{
    public class DeadState : IState
    {
        private Hunter _owner;

        public void Init(MonoBehaviour owner)
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
