using Common.Skill.Data;
using Data.Contents;
using System;
using System.Collections.Generic;
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
    public static DamageResultInfo CaculateDamage(SkillInfoScript script, Creature target, Creature attacker)
    {
        var def = Mathf.Max(0f, target.Defense - attacker.Penetration);

        var skillDamage = script.GetSkillLevelValue(attacker.Level).PPMToFloat();
        float damage = attacker.Atk * skillDamage;        //공격력 * 스킬 데미지

        bool isCritical = false;
        float criticalChancePer = UnityEngine.Random.Range(0f, 100f);
        if (criticalChancePer <= attacker.CriticalPercent)
        {
            isCritical = true;
            damage *= attacker.CriticalDamage + Managers.Data.GetConstValue(Define.ConstDefType.CriticalDamage);
        }

        damage -= def * Managers.Data.GetConstValue(Define.ConstDefType.DefenseCorrection);

        return new DamageResultInfo(damage, isCritical);
    }

    public static bool IsTarget(this Define.TargetType targetType, Creature my, Creature target)
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
