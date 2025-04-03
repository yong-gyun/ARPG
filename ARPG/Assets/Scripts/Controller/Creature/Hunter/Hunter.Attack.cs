using Common.State;
using UnityEngine;

namespace Creatures.HunterState
{
    public class AttackState : IState
    {
        public enum EATTACK_COMBO
        {
            ATTACK_1,
            ATTACK_2, 
            ATTACK_3,
            ATTACK_4,
        }

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
