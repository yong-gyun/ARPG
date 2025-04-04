using Creatures.HunterState;
using Data.Contents;
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

    public HunterInfo Info { get; set; }
    
    [SerializeField] private ESTATE _state;

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
        return true;
    }

    public async override void SetInfo(int creatureID)
    {
        Info = Managers.Data.GetHunterInfoDatas.Find(x => x.HunterID == creatureID);

        _model = await Managers.Resource.InstantiateAsync("Creature/Hunter", Info.PrefabName, transform);
        _anim = _model.GetComponent<Animator>();

        State = ESTATE.IDLE;
    }

    private void Update()
    {
        //TODO 스테이트 변경
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Dir = new Vector3(horizontal, 0f, vertical);

        _currentState.Update();
    }
}
