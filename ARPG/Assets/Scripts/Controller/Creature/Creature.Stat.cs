using System.Collections.Generic;
using UnityEngine;

public partial class Creature : MonoBehaviour
{
    protected Dictionary<Define.StatType, float> Buffs { get; private set; } = new Dictionary<Define.StatType, float>();
    public int Level { get; set; } = 1;
    public virtual float Hp { get { return _hp; } }
    public virtual float Mp { get { return _mp; } }
    public virtual float Atk { get { return _atk; } }
    public virtual float Defense { get { return _defense; } }
    public virtual float Penetration { get { return 0f; } }

    [SerializeField] protected int _templateID;
    [SerializeField] protected int _level = 1;
    [SerializeField] protected float _hp;           //±âº» ½ºÅÈ
    [SerializeField] protected float _mp;
    [SerializeField] protected float _atk;
    [SerializeField] protected float _defense;
    [SerializeField] protected float _speed;         //¾ê´Â °Á ÇÏµåÄÚµù °ª

    public float Speed { get { return _speed; } }
    public float CriticalPercent;
    public float CriticalDamage;

    public virtual void SetStat(int templateID)
    {
        var info = Managers.Data.GetBaseStatScripts.Find(x => x.TemplateID == templateID);
        _templateID = templateID;
        _hp = info.Hp;
        _mp = info.Mp;
        _atk = info.Attack;
        _defense = info.Defense;
    }
}
