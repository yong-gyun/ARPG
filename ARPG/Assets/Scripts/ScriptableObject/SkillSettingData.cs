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
        Animation,  //�ִϸ��̼ǿ� ���缭 ���
        Instante,   //�ִϸ��̼ǰ� ��� ���� ��� ���
    }
}

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/Skill", order = 0)]
public class SkillSettingData : ScriptableObject
{
    private Creature _owner;
    private GameObject _skillObject;

    public SkillActionData actionData;

    public Vector3 Dir 
    { 
        get 
        {
            Vector3 ret = Vector3.one;
            switch (_dir)
            {
                case SkillDir.Front:
                    ret = _owner.transform.forward;
                    break;
                case SkillDir.Back:
                    ret = -_owner.transform.forward;
                    break;
                case SkillDir.Right:
                    ret = _owner.transform.right;
                    break;
                case SkillDir.Left:
                    ret = -_owner.transform.right;
                    break;
            }

            return ret.normalized;
        } 
    }
    public Vector3 Offset { get { return _offset; } }
    public Vector3 Pos { get { return _pos; } }
    
    [SerializeField] private SkillDir _dir;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _pos;

    public void Init(Creature creature)
    {
        _owner = creature;
        _skillObject = actionData.skillObject;
        Effect effect = _skillObject.GetComponent<Effect>();
        effect?.Init(creature);
    }

    public Vector3 GetDir()
    {
        Vector3 ret = Vector3.one;

        switch (_dir)
        {
            case SkillDir.Front:
                ret = _owner.transform.forward;
                break;
            case SkillDir.Back:
                ret = -_owner.transform.forward;
                break;
            case SkillDir.Right:
                ret = _owner.transform.right;
                break;
            case SkillDir.Left:
                ret = -_owner.transform.right;
                break;
        }

        return ret.normalized;
    }
}
