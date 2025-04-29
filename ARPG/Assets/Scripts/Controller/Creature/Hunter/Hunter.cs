using Cysharp.Threading.Tasks;
using Data.Contents;
using System.Collections.Generic;
using UnityEngine;
using Common.State.Hunter;

namespace Common.State.Hunter
{
    public enum UseAttackType
    {
        Attack,
        Skill
    }

    public enum MoveType
    {
        Walk,
        Run,
        Dash
    }
}

public class Hunter : Creature
{
    private HunterStats _stats;
    [SerializeField] private MoveType _moveType;
    [SerializeField] private UseAttackType _useAttackType;
    private CharacterController _control;

    private Vector3 _lockDir;
    private float _curDashTime = 0f;
    private int _attackIndex;

    private void Awake()
    {
        _control = GetComponent<CharacterController>();
    }

    public override async UniTask Init(int templateID)
    {
        _stats = gameObject.GetOrAddComponent<HunterStats>();
        _stats.Init(templateID);

        await base.Init(templateID);

        BoxCollider collider = _model.GetComponent<BoxCollider>();
        _control.height = collider.size.y;
        _control.radius = collider.size.x;

        _colliderEvent = _model.GetOrAddComponent<ColliderEventHandler>();
    }

    protected override void OnUpdate(float deltaTime)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Dir = new Vector3(horizontal, 0f, vertical);

        base.OnUpdate(deltaTime);
    }

    protected override void UpdateIdle(float deltaTime)
    {
        if (Dir != Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _lockDir = Dir;
                _moveType = MoveType.Dash;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _moveType = MoveType.Run;
                }
                else
                {
                    _moveType = MoveType.Walk;
                }
            }

            ChangeState(Define.CreatureState.Move);
            return;
        }
    }

    protected override void UpdateMove(float deltaTime)
    {
        if (Dir == Vector3.zero)
        {
            ChangeState(Define.CreatureState.Idle);
            return;
        }

        if (_moveType != MoveType.Dash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _moveType = MoveType.Run;
                SetAnimation("Run");
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _moveType = MoveType.Walk;
                SetAnimation("Walk");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _moveType = MoveType.Dash;
                _lockDir = Dir;
                SetAnimation("Dash");
            }
        }

        switch (_moveType)
        {
            case MoveType.Walk:
                _control.Move(Dir * _stats.Speed * deltaTime);
                break;
            case MoveType.Run:
                _control.Move(Dir * _stats.RunSpeed * deltaTime);
                break;
            case MoveType.Dash:
                {
                    _curDashTime += deltaTime;
                    Debug.Log(transform.position);
                    if (_curDashTime >= _stats.DashTime)
                    {
                        ChangeState(Define.CreatureState.Idle);
                        return;
                    }

                    _control.Move(_lockDir.normalized * _stats.DashSpeed * deltaTime);
                }
                break;
        }
    }

    public override void ChangeState(Define.CreatureState state)
    {
        switch (state)
        {
            case Define.CreatureState.Idle:
                {
                    _curDashTime = 0f;
                    _moveType = MoveType.Walk;
                    SetAnimation("Idle");
                }
                break;
            case Define.CreatureState.Move:
                {
                    switch (_moveType)
                    {
                        case MoveType.Walk:
                            SetAnimation("Walk");
                            break;
                        case MoveType.Run:
                            SetAnimation("Run");
                            break;
                        case MoveType.Dash:
                            SetAnimation("Dash");
                            break;
                    }
                }
                break;
            case Define.CreatureState.Skill:
                {

                }
                break;
            case Define.CreatureState.Hit:
                {

                }
                break;
            case Define.CreatureState.Dead:
                {

                }
                break;
        }

        Debug.Log($"Change State: {_state} To {state}");
        _state = state;
    }
}
