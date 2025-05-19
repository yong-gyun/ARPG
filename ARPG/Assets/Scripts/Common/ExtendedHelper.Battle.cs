using Common.Skill.Data;
using Data.Contents;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Common.Skill.Data
{
    public class DamageResultInfo
    {
        public float damage;
        public bool critical;

        public DamageResultInfo(float damage, bool critical)
        {
            this.damage = damage;
            this.critical = critical;
        }
    }
}

public static partial class ExtendedHelper
{
    public static DamageResultInfo CaculateDamage(SkillLeveInfoScript script, Creature target, Creature attacker)
    {
        StringBuilder sb = new StringBuilder();

        var skillInfo = Managers.Data.GetSkillInfoScripts.Find(x => x.SkillID == script.SkillID);
        sb.AppendLine($"{skillInfo.SkillName} :\n{attacker.name} -> {target.name}");

        var def = Mathf.Max(0f, target.Defense - attacker.Penetration);

        var skillDamage = script.GetSkillLevelValue(attacker.Level).PPMToFloat();
        float damage = attacker.Atk * skillDamage;        //���ݷ� * ��ų ������

        sb.AppendLine($"��ų �����: {attacker.Atk} * {skillDamage} = {damage}");
        sb.AppendLine($"��� ����: {target.Defense} - {attacker.Penetration} = {def}");
        
        bool isCritical = false;
        float criticalChancePer = UnityEngine.Random.Range(0f, 100f);
        sb.AppendLine($"ũ��Ƽ�� Ȯ��: 0 ~ {attacker.CriticalPercent} = {criticalChancePer} " + (criticalChancePer <= attacker.CriticalPercent ? "(����)" : "(����)"));

        if (criticalChancePer <= attacker.CriticalPercent)
        {
            isCritical = true;
            damage *= attacker.CriticalDamage + Managers.Data.GetConstValue(Define.ConstDefType.CriticalDamage).PPMToFloat();
            sb.AppendLine($"ũ��Ƽ�� �����: 0 ~ {attacker.CriticalPercent} = {criticalChancePer} ");
        }

        //sb.AppendLine($"������ ����: {damage} - Mathf.Max(0, {def} * {Managers.Data.GetConstValue(Define.ConstDefType.DefenseCorrection).PPMToFloat()})");
        sb.AppendLine($"������ ���� (����� �ּҰ� 10): Mathf.Max(10f, {damage} - Mathf.Max(0f, {def}))");

        damage = Mathf.Max(10f, damage - Mathf.Max(0f, def));
        sb.AppendLine($"���� �����: {damage}");
        sb.AppendLine($"���� ü��: {target.Hp - damage}");
        Debug.Log(sb.ToString());

        return new DamageResultInfo(damage, isCritical);
    }

    public static bool IsTarget(this Creature target, Creature my, Define.TargetType targetType)
    {
        switch (targetType)
        {
            case Define.TargetType.Me:
                {
                    if (my == target)
                        return true;
                }
                break;
            case Define.TargetType.Monster:
                {
                    if (target.CreatureType == Define.CreatureType.Monster || target.CreatureType == Define.CreatureType.Boss)
                        return true;
                }
                break;
            case Define.TargetType.Boss:
                {
                    if (target.CreatureType == Define.CreatureType.Boss)
                        return true;
                }
                break;
            case Define.TargetType.Player:
                {
                    if (target.CreatureType == Define.CreatureType.Hunter)
                        return true;
                }
                break;
        }

        return false;
    }
}
