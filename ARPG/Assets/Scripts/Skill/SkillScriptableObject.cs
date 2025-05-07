using UnityEngine;
using Common.Skill;
using System;
using NUnit.Framework.Constraints;
using Cysharp.Threading.Tasks;

namespace Common.Skill
{
    [Serializable]
    public struct SkillActionData
    {
        public string animName;     //전환할 애니메이션
        public int delay;       //ms단위로 체크
        public SkillActionType actionType;
        public GameObject skillObject;
    }

    public enum SkillDir
    {
        Front,
        Back,
        Right,
        Left,
        Center
    }

    public enum SkillActionType
    {
        Instante,   //애니메이션과 상관 없이 즉시 사용
        Animation   //애니메이션에 맞춰서 사용
    }
}

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/Skill", order = 0)]
public class SkillScriptableObject : ScriptableObject
{
    public Define.SkillType skillTye;
    private Creature _owner;

    public SkillActionData actionData;
    [SerializeField] private SkillDir _dir;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _pos;

    public void Init(Creature creature)
    {
        _owner = creature;
    }

    public async void OnSkillAction()
    {
        
        Vector3 dir = GetDir();
        Vector3 pos = new Vector3(dir.x * _pos.x, _pos.y, dir.z * _pos.z);
        Quaternion qua = Quaternion.LookRotation(dir);
        await UniTask.Delay(actionData.delay);
        Managers.Object.InstantiateSkill(actionData.skillObject, pos + _offset, qua);
    }

    public Vector3 GetDir()
    {
        Vector3 ret = Vector3.zero;
        switch (_dir)
        {
            case SkillDir.Front:
                ret = _owner.transform.forward;
                break;
            case SkillDir.Back:
                ret = - _owner.transform.forward;
                break;
            case SkillDir.Right:
                ret = _owner.transform.right;
                break;
            case SkillDir.Left:
                ret = - _owner.transform.right;
                break;
        }

        return ret.normalized;
    }
}
