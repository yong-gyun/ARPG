using Cysharp.Threading.Tasks;
using Data.Contents;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract partial class Creature : BaseObject
{
    public Define.CreatureState State { get { return _state; } }

    public Define.CreatureType CreatureType { get; set; }

    public CreatureInfoScript Info { get; private set; }

    [SerializeField] protected Define.CreatureState _state;

    //[SerializeField] protected SkillEventHandler _skillEventHandler;

    public override bool Initialized()
    {
        if (base.Initialized() == false)
            return false;

        return true;
    }

    public Transform GetBone(HumanBodyBones humanBodyBonesType)
    {
        if (_anim == null)
            return null;

        return _anim.GetBoneTransform(humanBodyBonesType);
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);
        Define.CreatureType creatureType = TemplateID.GetCreatureType();
        Info = Managers.Data.GetCreatureInfoScripts.Find(info => info.TemplateID == TemplateID);
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        switch (State)
        {
            case Define.CreatureState.Idle:
                UpdateIdle(deltaTime);
                break;
            case Define.CreatureState.Move:
                UpdateMove(deltaTime);
                break;
            case Define.CreatureState.Skill:
                UpdateSkill(deltaTime);
                break;
            case Define.CreatureState.Hit:
                UpdateHit(deltaTime);
                break;
            case Define.CreatureState.Dead:
                UpdateDead(deltaTime);
                break;
        }
    }

    protected virtual void UpdateIdle(float deltaTime) { }
    protected virtual void UpdateMove(float deltaTime) { }
    protected virtual void UpdateSkill(float deltaTime) { }
    protected virtual void UpdateHit(float deltaTime) { }
    protected virtual void UpdateDead(float deltaTime) { }

    public abstract void ChangeState(Define.CreatureState state);

    public virtual void TakeDamage(SkillLeveInfoScript script, Creature attacker)
    {
        var damageInfo = ExtendedHelper.CaculateDamage(script, this, attacker);

        _hp -= damageInfo.damage;

        if (_hp <= 0f)
        {
            ChangeState(Define.CreatureState.Dead);
        }
        else
        {
            ChangeState(Define.CreatureState.Hit);
        }
    }
}