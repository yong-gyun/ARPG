using System;
using UnityEngine;

[Serializable]
public class Skill_Damage : Skill_Base
{
    public override void Apply(Creature target)
    {
        if (target.IsTarget(_owner, _targetType) == false)
            return;

        var levelScript = Managers.Data.GetSkillLeveInfoScripts.Find(x => x.SkillID == _skillID && x.SkillArg == _skillArg);
        target.TakeDamage(levelScript, _owner);
    }

    public override void Execute()
    {
        
    }

    public override void Exit(Creature target)
    {
        
    }

    public override void Release()
    {
        
    }
}