using UniRx;
using UnityEngine;

namespace Common.Anim
{
    public struct AnimStateInfo
    {
        public static AnimStateInfo Empty = new AnimStateInfo();

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
        public Define.SkillType SkillType { get { return _skillType; } }
        private Define.SkillType _prevSkillType;
        private SkillEventHandler _owner;

        [SerializeField] private Define.SkillType _skillType;

        private bool _init;

        public void Init(SkillEventHandler owner)
        {
            if (_init == true)
                return;

            _owner = owner;
            _init = true;
        }

        public void Clear()
        {
            _owner = null;
            _init = false;
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_init == false)
                return;

            _owner.OnSkillAnimStateEnter.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_init == false)
                return;

            _owner.OnSkillAnimStateUpdate.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_init == false)
                return;

            _owner.OnSkillAnimStateExit.OnNext(new AnimStateInfo(animator, stateInfo, layerIndex));
        }
    }
}