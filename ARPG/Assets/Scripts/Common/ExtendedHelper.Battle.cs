using Data.Contents;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static partial class ExtendedHelper
{
    public static float CalcuateDamage(SkillInfoScript script, Creature target, Creature attacker)
    {
        var def = Mathf.Max(0f, target.Defense - attacker.Penetration);

        var skillDamage = script.GetSkillLevelValue(attacker.Level).PPMToFloat();
        float damage = attacker.Atk * skillDamage;        //공격력 * 스킬 데미지

        return damage;
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
