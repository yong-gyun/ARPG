using UnityEngine;
using Common.State;

namespace Creatures.HunterState
{
    public class IdleState : IState
    {
        private Hunter _owner; 
        
        public void Init(MonoBehaviour owner)
        {
            _owner = owner.GetComponent<Hunter>();
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {
            
        }
    }
}
