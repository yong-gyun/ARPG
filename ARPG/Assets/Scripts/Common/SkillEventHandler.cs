using Common.Anim;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class SkillEventHandler : MonoBehaviour
{
    public Define.SkillType CurrentSkill { get; set; }

    private Dictionary<Define.SkillType, GameObject> _skillPoolDict = new Dictionary<Define.SkillType, GameObject>();

    [SerializeField] private SkillStateDatas _skillStateData;
        
    private Animator _animator;

    private Creature _owner;

    public Subject<(Define.SkillType currentSkillType, Define.SkillType prevSkillType, AnimStateInfo newStateInfo)> OnChangeSkillAnimState { get; private set; } = new Subject<(Define.SkillType currentSkillType, Define.SkillType prevSkillType, AnimStateInfo newStateInfo)>();

    public Subject<AnimStateInfo> OnSkillAnimStateEnter { get; private set; } = new Subject<AnimStateInfo>();

    public Subject<AnimStateInfo> OnSkillAnimStateUpdate { get; private set; } = new Subject<AnimStateInfo>();

    public Subject<AnimStateInfo> OnSkillAnimStateExit { get; private set; } = new Subject<AnimStateInfo>();
    
    public Subject<Unit> OnSkillAnimationEndEvent { get; private set; } = new Subject<Unit>();

    public void Init(Creature owner)
    {
        _owner = owner;
        _animator =  GetComponent<Animator>();

        _skillStateData.Init(owner);

        foreach (var item in _skillStateData.GetSkillSettingDataAll())
        {
            var clip = _animator.GetAnimationClip(item.actionData.animName);
            clip.RegisterAnimationEvent("OnSkillAnimationEnd", clip.length);
        }

        var animStateBehaviours = _animator.GetBehaviours<SkillAnimStateBehaviour>();
        foreach (var item in animStateBehaviours)
            item.Init(this);
    }

    public SkillSettingData GetCurrentSkillSettingData()
    {
        return _skillStateData.GetSkillSettingData(CurrentSkill);
    }

    public async void SkillAction(int command)
    {
        GameObject go = await GetSkillObject(CurrentSkill);
        Effect effect = go.GetComponent<Effect>();
        var skills = go.GetComponentsInChildren<Skill>().ToList();
        var skillID = GetCurrentSkillID();

        effect.Init(_owner);

        foreach (var mit in skills)
            mit.Init(_owner, skillID);

        Debug.Log("Action Effect");
        effect.PlayAction(command);
    }

    private async UniTask<GameObject> GetSkillObject(Define.SkillType skillType)
    {
        if (_skillPoolDict.TryGetValue(skillType, out var ret) == true)
        {
            if (ret != null)
                return ret;
        }

        SkillSettingData skillData = _skillStateData.GetSkillSettingData(CurrentSkill);
        if (skillData != null)
        {
            GameObject go = await skillData.CreateSkill(_owner);
            _skillPoolDict[skillType] =  go;

            return go;
        }

        return null;
    }
    public void OnSkillAnimationEnd()
    {
        Debug.Log($"On Skill End: caller {gameObject.name}");
        OnSkillAnimationEndEvent.OnNext(Unit.Default);
    }

    private int GetCurrentSkillID()
    {
        switch (CurrentSkill)
        {
            case Define.SkillType.Passive:
                return _owner.Info.PassiveSkillID;

            case Define.SkillType.Combat_Attack_1:
                return _owner.Info.CombatAttack1ID;

            case Define.SkillType.Combat_Attack_2:
                return _owner.Info.CombatAttack2ID;
            
            case Define.SkillType.Combat_Attack_3:
                return _owner.Info.CombatAttack3ID;
            
            case Define.SkillType.Combat_Attack_4:
                return _owner.Info.CombatAttack4ID;

            case Define.SkillType.NormalSkill_1:
                return _owner.Info.NormalSkill1ID;

            case Define.SkillType.NormalSkill_2:
                return _owner.Info.NormalSkill2ID;

            case Define.SkillType.UltSkill:
                return _owner.Info.UltSkillID;

            default:
                return 0;
        }
    }
}
