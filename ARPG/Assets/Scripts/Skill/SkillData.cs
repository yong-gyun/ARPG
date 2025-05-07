using UnityEngine;
using Common.Skill;
using System;
using NUnit.Framework.Constraints;
using Cysharp.Threading.Tasks;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.UI.Image;

namespace Common.Skill
{
    [Serializable]
    public struct SkillActionData
    {
        public string animName;     //��ȯ�� �ִϸ��̼�
        public int delay;       //ms������ üũ
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
        Instante,   //�ִϸ��̼ǰ� ��� ���� ��� ���
        Animation   //�ִϸ��̼ǿ� ���缭 ���
    }
}

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/Skill", order = 0)]
public class SkillData : ScriptableObject
{
    public Define.SkillType skillTye;
    private Creature _owner;
    private GameObject _skillObject;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _pos;

    public SkillActionData actionData;
    [SerializeField] private SkillDir _dir;

    public void Init(Creature creature)
    {
        _owner = creature;
    }

    public Vector3 GetPos() { return _offset + _pos; }

    public Vector3 GetDir()
    {
        Vector3 ret = Vector3.zero;
        switch (_dir)
        {
            case SkillDir.Front:
                ret = _skillObject.transform.forward;
                break;
            case SkillDir.Back:
                ret = -_skillObject.transform.forward;
                break;
            case SkillDir.Right:
                ret = _skillObject.transform.right;
                break;
            case SkillDir.Left:
                ret = -_skillObject.transform.right;
                break;
        }

        return ret.normalized;
    }
}
