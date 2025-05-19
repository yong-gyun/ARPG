using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("스킬이 사용 되는 즉시 호출")]
    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _executeSkillEffects = new List<Skill_Base>();     //스킬 사용 즉시 호출

    [Header("스킬이 대상에게 적중했을 때 호출 (충돌 등)")]
    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _applySkills = new List<Skill_Base>();       //스킬이 대상에게 적용될 때 호출

    [Header("스킬이 종료될 시 호출")]
    [SerializeReference, SubclassSelector]
    private List<Skill_Base> _completeSkillEffects = new List<Skill_Base>();    //스킬 종료 시 호출

    [SerializeField] private List<Creature> _targets = new List<Creature>();    //스킬 적용 대상 (나 자신도 포함됨)

    public Creature Owner { get; private set; }

    public void Init(Creature owner, int skillID)
    {
        Owner = owner;

        foreach (var mit in _executeSkillEffects)
            mit.Init(owner, skillID);

        foreach (var mit in _applySkills)
            mit.Init(owner, skillID);

        foreach (var mit in _completeSkillEffects)
            mit.Init(owner, skillID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            Creature creature = other.GetComponent<Creature>();
            if (creature != null)
            {
                _targets.Add(creature);
                foreach (var skill in _applySkills)
                    skill.Apply(creature);
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
                    foreach (var skill in _applySkills)
                        skill.Exit(target);
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
            if (skill.executeType == Define.SkillExecuteType.Instante)
            {
                skill.Apply(Owner);
                foreach (var target in _targets)
                    skill.Apply(target);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var skill in _executeSkillEffects)
            skill.Release();

        foreach (var skill in _completeSkillEffects)
        {
            skill.Execute();

            skill.Apply(Owner);
            foreach (var target in _targets)
                skill.Apply(target);
        }

        _targets.Clear();
    }
}
