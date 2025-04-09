using Common.State;
using UnityEngine;

namespace Creatures.HunterState
{
    public class MoveState : IState
    {
        private CharacterController _characterControl;
        private Hunter _owner;
        private float _speed;

        public void Init(MonoBehaviour owner)
        {
            _owner = owner.GetComponent<Hunter>();
            _speed = _owner.Stats.Speed;
            _characterControl = owner.GetComponent<CharacterController>();
        }

        public void Enter()
        {
            _owner.SetAnimation("Move");
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_owner.Dir == Vector3.zero)
            {
                _owner.State = Hunter.ESTATE.IDLE;
                return;
            }

            _characterControl.Move(_owner.Dir * _speed);
        }
    }
}
