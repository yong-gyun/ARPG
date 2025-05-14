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

    public List<SkillSettingData> GetSkillSettingDataAll()
    {
        return _skillStateDict.Values.ToList();
    }
}