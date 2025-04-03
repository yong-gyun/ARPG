using UnityEngine;

namespace Common.State
{
    public interface IState
    {
        public void Init(MonoBehaviour owner);
        public void Enter();
        public void Update();
        public void Exit();
    }
}
