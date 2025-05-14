using UniRx;
using UnityEngine;

public class SkillEventHandler : MonoBehaviour
{
    public Define.SkillType CurrentSkill { get; set; }

    [SerializeField] protected SkillStateDatas _skillStateData;

    private Creature _owner;

    private Animator _anim;

    public Subject<Unit> OnSkillAnimationEndEvent { get; private set; } = new Subject<Unit>();

    public void Init(Creature owner)
    {
        _owner = owner;
        _anim = GetComponent<Animator>();
        _skillStateData.Init(owner);

        foreach (var item in _skillStateData.GetSkillSettingDataAll())
        {
            var clip = _anim.GetAnimationClip(item.actionData.animName);
            clip.RegisterAnimationEvent("OnSkillEnd", clip.length);
        }
    }

    public SkillSettingData GetCurrentSkillSettingData()
    {
        return _skillStateData.GetSkillSettingData(CurrentSkill);
    }

    public async void SkillAction(int command)
    {
        SkillSettingData skillData = _skillStateData.GetSkillSettingData(CurrentSkill);
        if (skillData != null)
        {
            GameObject go = await skillData.CreateSkill();
            Effect effect = go.GetComponent<Effect>();
            effect.PlayAction(command);
        }
    }

    public void OnSkillEnd()
    {
        OnSkillAnimationEndEvent.OnNext(Unit.Default);
    }
}
