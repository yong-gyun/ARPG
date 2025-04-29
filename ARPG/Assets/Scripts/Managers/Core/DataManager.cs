using System.Collections.Generic;
using UnityEngine;
using Data.Contents;
using System.Linq;

public partial class DataManager
{
    public Dictionary<int, List<SkillInfoScript>> SkillInfoDict { get; private set; } = new Dictionary<int, List<SkillInfoScript>>();

    public void Init()
    {
        foreach (var item in GetSkillInfoScripts)
        {
            if (SkillInfoDict.ContainsKey(item.SkillID) == false)
                SkillInfoDict[item.SkillID] = new List<SkillInfoScript>();

            SkillInfoDict[item.SkillID].Add(item);
        }
    }

    public List<SkillInfoScript> GetSkillArgs(int skillID)
    {
        if (SkillInfoDict.TryGetValue(skillID, out var ret) == true)
            return ret;

        return null;
    }

    public SkillInfoScript GetSkillArg(int skillID, int skillArg)
    {
        if (SkillInfoDict.TryGetValue(skillID, out var ret) == true)
            return ret.Find(x => x.SkillArg == skillArg);

        return null;
    }

    public long GetSkillValue(int level, int skillID, int skillArg)
    {
        if (SkillInfoDict.TryGetValue(skillID, out var scripts) == true)
        {
            var ret = scripts.Find(x => x.SkillArg == skillArg);
            if (ret != null)
                return ret.GetSkillLevelValue(level);
        }

        return 0;
    }

    public int GetConstValue(Define.ConstDefType constDefType)
    {
        var script = GetConstValueScripts.Find(x => x.ConstType == constDefType);
        if (script == null)
            return 0;

        return script.Value;
    }
}