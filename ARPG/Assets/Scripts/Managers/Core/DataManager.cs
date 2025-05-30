using System.Collections.Generic;
using UnityEngine;
using Data.Contents;
using System.Linq;

public partial class DataManager
{
    public Dictionary<int, List<SkillInfoScript>> SkillInfoDict { get; private set; } = new Dictionary<int, List<SkillInfoScript>>();
    public bool Loaded { get; set; }

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

    public int GetConstValue(Define.ConstDefType constDefType)
    {
        var script = GetConstValueScripts.Find(x => x.ConstType == constDefType);
        if (script == null)
            return 0;

        return script.Value;
    }
}