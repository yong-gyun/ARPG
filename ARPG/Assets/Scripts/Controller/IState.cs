using UnityEngine;

namespace Common.State
{
    public interface IState
    {
        public Creature GetOwner();
        public Vector3 GetDir();

        public void Init(Creature owner);
        public void Enter();
        public void Update();
        public void Exit();
    }
}
