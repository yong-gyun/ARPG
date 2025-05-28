using UnityEngine;
using Common.Skill;
using System;

namespace Common.Skill
{
    public class SkillData
    {
        public int SkillID {  get; set; }
        public float CurrentCooldown { get; set; }
        public float Cooldown { get; set; }
    }

    [Serializable]
    public struct SkillActionData
    {
        public string animName;     //전환할 애니메이션
        public int delay;       //ms단위로 체크
        public SkillActionType actionType;
        public GameObject skillObject;
    }

    public enum SkillActionType
    {
        Animation,  //애니메이션에 맞춰서 사용
        Instante,   //애니메이션과 상관 없이 즉시 사용
    }
}

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/Skill", order = 0)]
public class SkillSettingData : ScriptableObject
{
    private Creature _owner;
    private GameObject _skillObject;

    public SkillActionData actionData;

    public Vector3 Dir { get { return _owner.gameObject.GetLocalDir(_dir); } }

    public Vector3 Offset { get { return _offset; } }

    public Vector3 Pos { get { return _pos; } }
    
    [SerializeField] private Define.DirType _dir;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _pos;

    public void Init(Creature creature)
    {
        _owner = creature;
        _skillObject = actionData.skillObject;
    }
}
