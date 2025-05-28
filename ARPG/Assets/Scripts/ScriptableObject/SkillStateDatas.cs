using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic.Serialized;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillStateData", menuName = "ScriptableObject/SkillStateData", order = 0)]
public class SkillStateDatas : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<Define.SkillType, SkillSettingData> _skillStateDict = new SerializedDictionary<Define.SkillType, SkillSettingData>();

    public void Init(Creature owner)
    {
        foreach (var mit in _skillStateDict.Values)
            mit.Init(owner);
    }

    public SkillSettingData GetSkillSettingData(Define.SkillType skillType)
    {
        return _skillStateDict[skillType];
    }

    public Dictionary<Define.SkillType, SkillSettingData> GetSkillSettingPairDataAll()
    {
        Dictionary<Define.SkillType, SkillSettingData> ret = new Dictionary<Define.SkillType, SkillSettingData>();
        foreach ((Define.SkillType, SkillSettingData) mit in _skillStateDict)
            ret.Add(mit.Item1, mit.Item2);

        return ret;
    }

    public List<SkillSettingData> GetSkillSettingDataAll()
    {
        return _skillStateDict.Values.ToList();
    }
}