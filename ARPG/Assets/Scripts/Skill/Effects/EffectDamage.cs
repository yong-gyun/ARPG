using UnityEngine;

public class EffectDamage : EffectSkillBase
{
    [SerializeField] private int _skillArg;

    public override void Start()
    {
        var script = Managers.Data.GetSkillArg(_parent.SkillID, _skillArg);
        foreach (var item in _targets)
            item.TakeDamage(script, _parent.Owner);
    }
}