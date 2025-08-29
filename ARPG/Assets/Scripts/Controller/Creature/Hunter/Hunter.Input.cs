using System;
using UniRx;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Common.State.Hunter;

public partial class Hunter : Creature
{
    [SerializeField] private Define.SkillType _nextSkillType;
    private float _horizontal;
    private float _vertical;

    private bool IsNormalAttack(Define.SkillType skillType)
    {
        if (skillType == Define.SkillType.Combat_Attack_1 ||
            skillType == Define.SkillType.Combat_Attack_2 ||
            skillType == Define.SkillType.Combat_Attack_3 ||
            skillType == Define.SkillType.Combat_Attack_4)
            return true;

        return false;
    }
}
