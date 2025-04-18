using Creatures.HunterState;
using Cysharp.Threading.Tasks;
using Data.Contents;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Hunter : Creature
{
    public enum ESTATE
    {
        NONE,
        IDLE,
        MOVE,
        ATTACK,
        SKILL,
        HIT,
        DASH,
        DEAD,
    }

    public ESTATE State 
    { 
        get 
        { 
            return _state;
        }
        set
        {
            if (_state != value)
            {
                _state = value;
                ChangeState(_state);
            }
        }
    }

    [SerializeField] private ESTATE _state = ESTATE.NONE;

    public async override UniTask Init(int templateID)
    {
        if (_init == true)
            return;

        await base.Init(templateID);

        AddState(ESTATE.IDLE, new IdleState());
        AddState(ESTATE.MOVE, new MoveState());
        AddState(ESTATE.DASH, new DashState());
        AddState(ESTATE.SKILL, new SkillState());
        AddState(ESTATE.ATTACK, new AttackState());
        AddState(ESTATE.HIT, new HitState());
        AddState(ESTATE.DEAD, new DeadState());

        State = ESTATE.IDLE;
    }

    private void FixedUpdate()
    {
        if (_init == false)
            return;

        if (_currentState != null)
            _currentState.Update();
    }
}
