using System.Collections.Generic;
using UnityEngine;
using Data.Contents;
using System.Linq;

public partial class DataManager
{
    public Dictionary<int, List<EffectInfoScript>> EffectInfoDict { get; private set; } = new Dictionary<int, List<EffectInfoScript>>();

    public void Init()
    {
        foreach (var item in GetEffectInfoScripts)
        {
            if (EffectInfoDict.ContainsKey(item.SkillID) == false)
                EffectInfoDict[item.SkillID] = new List<EffectInfoScript>();

            EffectInfoDict[item.SkillID].Add(item);
        }
    }

    public List<EffectInfoScript> GetSkillArgs(int skillID)
    {
        if (EffectInfoDict.TryGetValue(skillID, out var ret) == true)
            return ret;

        return null;
    }

    public EffectInfoScript GetSkillArg(int skillID, int skillArg)
    {
        if (EffectInfoDict.TryGetValue(skillID, out var ret) == true)
            return ret.Find(x => x.SkillArg == skillArg);

        return null;
    }

    public int GetSkillValue(int level, int skillID, int skillArg)
    {
        if (EffectInfoDict.TryGetValue(skillID, out var scripts) == true)
        {
            var ret = scripts.Find(x => x.SkillArg == skillArg);
            if (ret != null)
                return ret.GetSkillLevelValue(level);
        }

        return 0;
    }

    public float GetConstValue(Define.ConstDefType constDefType)
    {
        var script = GetConstValueScripts.Find(x => x.ConstType == constDefType);
        if (script == null)
            return 0f;

        return script.Value;
    }
}