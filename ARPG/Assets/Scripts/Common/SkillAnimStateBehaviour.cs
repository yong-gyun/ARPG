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

    public class SkillAnimStateBehaviour : StateMachineBehaviour
    {
        public Subject<AnimStateInfo> OnStateEnterListener { get; private set; } = new Subject<AnimStateInfo>();
        public Subject<AnimStateInfo> OnStateUpdateListener { get; private set; } = new Subject<AnimStateInfo>();
        public Subject<AnimStateInfo> OnStateExitListener { get; private set; } = new Subject<AnimStateInfo>();
        public Subject<(AnimStateInfo prevAnimInfo, AnimStateInfo newAnimInfo)> OnStateChangeListener { get; private set; } = new Subject<(AnimStateInfo prevnewAnimInfo, AnimStateInfo newAnimInfo)>();
        public Define.SkillType SkillType { get { return _skillType; } }
        public AnimStateInfo CurrentAnimStateInfo { get { return _currentAnimStateInfo; } }

        private AnimStateInfo _currentAnimStateInfo;
        private AnimStateInfo _prevAnimInfo;
        [SerializeField] private Define.SkillType _skillType;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var temp = _currentAnimStateInfo;
            _currentAnimStateInfo = new AnimStateInfo(animator, stateInfo, layerIndex);

            if (_prevAnimInfo != null)
            {
                _prevAnimInfo = _currentAnimStateInfo;
                OnStateChangeListener.OnNext((_prevAnimInfo, _currentAnimStateInfo));
            }
            else if (_prevAnimInfo == null && temp != null)
            {
                _prevAnimInfo = _currentAnimStateInfo;
                
            }

            
            if (OnStateEnterListener.HasObservers == false)
                return;

            OnStateEnterListener.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (OnStateUpdateListener.HasObservers == false)
                return;

            OnStateUpdateListener.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (OnStateExitListener.HasObservers == false)
                return;

            OnStateExitListener.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }
    }
}