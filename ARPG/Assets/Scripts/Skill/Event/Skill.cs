using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skill : MonoBehaviour
{
    public enum ExecuteType
    {
        Colision,   //����Ʈ�� �浹��
        Instante,   //�浹 ��� ���� ��� �ߵ�
    }

    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _executeSkillEffects = new List<Skill_Base>();     //��ų ��� ��� ȣ��

    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _applySkillEffects = new List<Skill_Base>();       //��ų�� ��󿡰� ����� �� ȣ��

    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _completeSkillEffects = new List<Skill_Base>();    //��ų ���� �� ȣ��

    [SerializeField] private List<Creature> _targets = new List<Creature>();    //��ų ���� ��� (�� �ڽŵ� ���Ե�)

    public Creature Owner { get; private set; }

    public void Init(Creature owner, int skillID)
    {
        Owner = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            Creature target = other.GetComponent<Creature>();
            if (target != null)
            {
                _targets.Add(target);
                foreach (var skill in _applySkillEffects)
                    skill.Apply(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            Creature target = other.GetComponent<Creature>();
            if (target != null)
            {
                if (_targets.Contains(target) == true)
                {
                    _targets.Remove(target);
                }
            }
        }
    }

    private void OnEnable()
    {
        _targets.Clear();

        foreach (var skill in _executeSkillEffects)
        {
            skill.Execute();
            if (skill.executeType == ExecuteType.Instante)
            {
                skill.Apply(Owner);
                foreach (var target in _targets)
                    skill.Apply(target);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var skill in _completeSkillEffects)
        {
            skill.Execute();

            skill.Apply(Owner);
            foreach (var target in _targets)
                skill.Apply(target);
        }
    }
}
