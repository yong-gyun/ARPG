using Creatures.HunterState;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Creature
{
    public enum ESTATE
    {
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
                SetAnimation(_state);
            }
        }
    }

    private ESTATE _state;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        AddState(ESTATE.IDLE, new IdleState());
        AddState(ESTATE.MOVE, new MoveState());
        AddState(ESTATE.DASH, new DashState());
        AddState(ESTATE.SKILL, new SkillState());
        AddState(ESTATE.ATTACK, new AttackState());
        AddState(ESTATE.HIT, new HitState());
        AddState(ESTATE.DEAD, new DeadState());
        
        State = ESTATE.IDLE;
        return true;
    }

    private void Update()
    {
        //TODO 스테이트 변경
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Dir = new Vector3(horizontal, 0f, vertical);

        switch (State)
        {
            case ESTATE.IDLE:
                {
                    if (Dir != Vector3.zero)
                    {
                        State = ESTATE.MOVE;
                    }
                }
                break;
        }
    }
}
