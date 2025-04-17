using Common.State;
using UnityEngine;

namespace Creatures.HunterState
{
    public class MoveState : IState
    {
        private Rigidbody _rigidbody;
        private Hunter _owner;

        public Creature GetOwner() { return _owner; }
        public Vector3 GetDir() { return _owner.Dir; }
        private float GetSppeed() { return _owner.Stats == null ? 0f : _owner.Stats.Speed; }

        public void Init(Creature owner)
        {
            _owner = owner.GetComponent<Hunter>();
            _rigidbody = owner.GetComponent<Rigidbody>();
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
            if (GetDir() == Vector3.zero)
            {
                _owner.State = Hunter.ESTATE.IDLE;
                return;
            }

            Quaternion qua = Quaternion.LookRotation(GetDir());
            _owner.transform.rotation = Quaternion.Slerp(_owner.transform.rotation, qua, 180f * Time.deltaTime);
            _rigidbody.linearVelocity = GetDir() * GetSppeed() * Time.deltaTime;
        }
    }
}
