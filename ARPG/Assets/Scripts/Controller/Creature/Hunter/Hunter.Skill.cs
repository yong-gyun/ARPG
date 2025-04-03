using UnityEngine;
using Common.State;

namespace Creatures.HunterState
{
    public class SkillState : IState
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
