using Data.Contents;
using UnityEngine;

public partial class Hunter : Creature
{
    public override float Hp { get { return GetStatValue(_hp, _levelStat == null ? 0 : _levelStat.Hp); } }
    public override float Mp { get { return GetStatValue(_mp, _levelStat == null ? 0 : _levelStat.Mp); } }
    public override float Atk { get { return GetStatValue(_atk, _levelStat == null ? 0 : _levelStat.Attack); } }
    public override float Defense { get { return GetStatValue(_defense, _levelStat == null ? 0 : _levelStat.Defense); } }

    public float SkillCoooldown;            //½ºÅ³ Äð°¨
    
    public float RunSpeed { get; private set; }
    public float DashSpeed { get; private set; }
    public float DashTime { get; private set; }
    public float DashDistance { get; private set; }

    private HunterLevelStatScript _levelStat;

    public override void SetStat(int templateID)
    {
        base.SetStat(templateID);

        _speed = Managers.Data.GetConstValue(Define.ConstDefType.HunterMoveSpeed);
        RunSpeed = _speed * Managers.Data.GetConstValue(Define.ConstDefType.RunSpeed).PPMToFloat();
        DashTime = Managers.Data.GetConstValue(Define.ConstDefType.DashTime).PPMToFloat();
        DashDistance = Managers.Data.GetConstValue(Define.ConstDefType.DashDistance).PPMToFloat();
        DashSpeed = DashDistance / DashTime;
    }

    public float GetStatValue(float baseStat, float levelStat)
    {
        return baseStat + (levelStat * _level);
    }
}
