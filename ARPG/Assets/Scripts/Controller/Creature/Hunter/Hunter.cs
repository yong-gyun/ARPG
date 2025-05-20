using UniRx;
using System;
using Data.Contents;
using System.Collections.Generic;
using UnityEngine;
using Common.State.Hunter;
using Cysharp.Threading.Tasks;
using Common.Input;

namespace Common.State.Hunter
{
    public enum MoveType
    {
        Walk,
        Run,
        Dash
    }
}

public partial class Hunter : Creature
{
    [SerializeField] private MoveType _moveType;

    private CharacterController _control;
    private CameraController _cameraControl;

    private bool _actionFlag;

    private Vector3 _lockDir;
    [SerializeField] private float _curDashTime = 0f;
    private bool _isReservedNextAttack;
    private float _animClipTime = 0f;
    private float _currentAnimClipTime = 0f;

    private void Awake()
    {
        _cameraControl = (Managers.Scene.CurrentScene as GameScene).GetCameraController;
        _control = GetComponent<CharacterController>();

        BindMouseInputEvent();
        BindKeyInputEvent();
    }

    public override async UniTask Init(int templateID)
    {
        await base.Init(templateID);

        BoxCollider collider = _model.GetComponent<BoxCollider>();
        _control.height = collider.size.y;
        _control.radius = collider.size.x;

        //_skillEvent.OnSkillAnimationEndEvent.Subscribe(_ =>
        //{
        //    if (_nextSkillType == Define.SkillType.None || _nextSkillType == _skillEvent.CurrentSkill)
        //    {
        //        ChangeState(Define.CreatureState.Idle);
        //    }
        //    else
        //    {
        //        _skillEvent.CurrentSkill = _nextSkillType;
        //        ChangeState(Define.CreatureState.Skill);
        //    }
        //});

        _colliderEvent = _model.GetOrAddComponent<ColliderEventHandler>();
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (_control.isGrounded == false)
            _control.Move(Vector3.down * 9.8f * deltaTime);       

        Dir = _cameraControl.Forward * _vertical + _cameraControl.Right * _horizontal;
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

        transform.forward = new Vector3(_cameraControl.transform.right.x, 0f, _cameraControl.transform.forward.z).normalized;
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
                _control.Move(Dir * Speed * deltaTime);
                break;
            case MoveType.Run:
                _control.Move(Dir * RunSpeed * deltaTime);
                break;
            case MoveType.Dash:
                {
                    _curDashTime += deltaTime;
                    if (_curDashTime >= DashTime)
                    {
                        ChangeState(Define.CreatureState.Idle);
                        return;
                    }

                    _control.Move(_lockDir.normalized * DashSpeed * deltaTime);
                }
                break;
        }

        Vector3 dir = _moveType != MoveType.Dash ? Dir : _lockDir.normalized;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 10f);
    }


    protected override void UpdateSkill(float deltaTime)
    {
        //이벤트가 타이밍이 안맞아 종종 씹히는 현상이 있을 수 있으니 여기선 애니메이션 종료 되었는지 체크하는 방어 코드 추가
        _currentAnimClipTime += deltaTime;
        if (_currentAnimClipTime >= _animClipTime)
        {
            if (_nextSkillType == Define.SkillType.None)
            {
                _currentAnimClipTime = 0f;
                ChangeState(Define.CreatureState.Idle);
            }
        }

        //1.   현재 콤보 어택이 다음으로 예약된 콤보 어택이랑 같은지 체크
        //2-1. 만약 None이 오거나 애니메이션이 끝날 때까지 예약된 콤보 어택이 계속 똑같으면 idle로 전환 (이건 되어 있음 이벤트로)
        //2-2. 
    }

    public override void ChangeState(Define.CreatureState state)
    {
        switch (state)
        {
            case Define.CreatureState.Idle:
                {
                    _curDashTime = 0f;
                    _moveType = MoveType.Walk;
                    _skillEvent.CurrentSkill = Define.SkillType.None;
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
                    var skillSetting = _skillEvent.GetCurrentSkillSettingData();
                    _animClipTime = _anim.GetAnimationClip(skillSetting.actionData.animName).length;
                    SetAnimation(skillSetting.actionData.animName, 0f);
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
