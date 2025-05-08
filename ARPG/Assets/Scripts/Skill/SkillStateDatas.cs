using System;
using System.Collections.Generic.Serialized;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillStateData", menuName = "ScriptableObject/SkillStateData", order = 0)]
public class SkillStateDatas : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<Define.SkillType, SkillData> _skillStateDict = new SerializedDictionary<Define.SkillType, SkillData>();

    public void Init(Creature owner)
    {
        foreach (var mit in _skillStateDict.Values)
            mit.Init(owner);
    }

    public SkillData GetSkillData(Define.SkillType skillType)
    {
        return _skillStateDict[skillType];
    }
}