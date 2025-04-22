using Data.Contents;
using UnityEngine;

public class HunterStats : Stats
{
    public int Hp { get { return GetStatValue(_hp, _levelStat == null ? 0 : _levelStat.Hp); } }
    public int Mp { get { return GetStatValue(_mp, _levelStat == null ? 0 : _levelStat.Mp); } }
    public int Atk { get { return GetStatValue(_atk, _levelStat == null ? 0 : _levelStat.Attack); } }
    public int Defense { get { return GetStatValue(_defense, _levelStat == null ? 0 : _levelStat.Defense); } }

    public int SkillCoooldown;
    public int CriticalPercent;
    public int CriticalDamage;
    public int Penetration;

    public float RunSpeed { get; private set; }
    public float DashSpeed { get; private set; }
    public float DashTime { get; private set; }
    public float DashDistance { get; private set; }

    private HunterLevelStatScript _levelStat;

    public override void Init(int templateID)
    {
        base.Init(templateID);

        RunSpeed = _speed * Managers.Data.GetConstValue(Define.ConstDefType.RunSpeed);
        DashTime = Managers.Data.GetConstValue(Define.ConstDefType.DashTime);
        DashDistance = Managers.Data.GetConstValue(Define.ConstDefType.DashDistance);
        DashSpeed = DashDistance / DashTime;
    }

    public override void SetLevel(int level)
    {
        _levelStat = Managers.Data.GetHunterLevelStatScripts.Find(x => x.HunterID == _templateID.GetCreatureID());
    }

    public int GetStatValue(int baseStat, int levelStat)
    {
        return baseStat + (levelStat * _level);
    }
}
