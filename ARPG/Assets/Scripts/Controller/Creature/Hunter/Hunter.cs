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

    public CharacterController CharacterControl { get; set; }

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

        OnSetInfoCallback.Subscribe(_ =>
        {
            CharacterControl = gameObject.GetOrAddComponent<CharacterController>();

            CharacterController temp = _model.GetComponent<CharacterController>();
            CharacterControl.center = temp.center;
            CharacterControl.radius = temp.radius;
            CharacterControl.height = temp.height;

            Destroy(temp);
            State = ESTATE.IDLE;
        });

        return true;
    }

    private void Update()
    {
        if (_init == false)
            return;

        if (CharacterControl == null)
            return;

        //TODO 스테이트 변경
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Dir = new Vector3(horizontal, 0f, vertical);

        if (_currentState != null)
            _currentState.Update();

        if (CharacterControl.isGrounded == false)
        {
            CharacterControl.Move(Vector3.down * 9.8f);
        }
    }
}
