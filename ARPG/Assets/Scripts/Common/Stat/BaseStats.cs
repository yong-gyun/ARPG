using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseStats : MonoBehaviour
{
    protected Dictionary<Define.StatType, float> _additionalStats = new Dictionary<Define.StatType, float>();

    [SerializeField] protected int _templateID;
    [SerializeField] protected int _level;
    [SerializeField] protected int _hp;           //±âº» ½ºÅÈ
    [SerializeField] protected int _mp;
    [SerializeField] protected int _atk;
    [SerializeField] protected int _defense;
    [SerializeField] protected float _speed;         //¾ê´Â °Á ÇÏµåÄÚµù °ª

    public virtual void Init(int templateID)
    {
        var info = Managers.Data.GetBaseStatDatas.Find(x => x.TemplateID == templateID);
        _templateID = templateID;
        _hp = info.Hp;
        _mp = info.Mp;
        _atk = info.Attack;
        _defense = info.Defense;
    }

    public abstract void SetLevel(int level);
}