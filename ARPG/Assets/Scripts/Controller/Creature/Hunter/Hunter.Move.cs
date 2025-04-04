using Common.State;
using UnityEngine;

namespace Creatures.HunterState
{
    public class MoveState : IState
    {
        private Hunter _owner;
        private float _speed;

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
            if (_owner.Dir == Vector3.zero)
            {
                _owner.State = Hunter.ESTATE.IDLE;
                return;
            }

            _owner.transform.position += _owner.Dir * _speed * Time.deltaTime;
        }
    }
}
