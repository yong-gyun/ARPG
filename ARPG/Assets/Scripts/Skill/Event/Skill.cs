using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skill : MonoBehaviour
{
    public enum ExecuteType
    {
        Colision,   //이펙트에 충돌시
        Instante,   //충돌 상관 없이 즉시 발동
    }

    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _executeSkillEffects = new List<Skill_Base>();     //스킬 사용 즉시 호출

    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _applySkillEffects = new List<Skill_Base>();       //스킬이 대상에게 적용될 때 호출

    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _completeSkillEffects = new List<Skill_Base>();    //스킬 종료 시 호출

    [SerializeField] private List<Creature> _targets = new List<Creature>();    //스킬 적용 대상 (나 자신도 포함됨)

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
