using UniRx;
using UnityEngine;

namespace Common.Anim
{
    public class AnimStateInfo
    {
        public Animator animator;
        public AnimatorStateInfo stateInfo;
        public int layer;

        public AnimStateInfo(Animator animator, AnimatorStateInfo stateInfo, int layer)
        {
            this.animator = animator;
            this.stateInfo = stateInfo;
            this.layer = layer;
        }

        public bool CheckAnimName(string animName)
        {
            bool ret = stateInfo.IsName(animName);
            return ret;
        }
    }

    public class CreatureAnimStateBehaviour : StateMachineBehaviour
    {
        public Subject<AnimStateInfo> OnStateEnterListener;
        public Subject<AnimStateInfo> OnStateUpdateListener;
        public Subject<AnimStateInfo> OnStateExitListener;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (OnStateEnterListener == null)
                return;

            OnStateEnterListener.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (OnStateUpdateListener == null)
                return;

            OnStateUpdateListener.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (OnStateExitListener == null)
                return;

            OnStateExitListener.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }
    }
}