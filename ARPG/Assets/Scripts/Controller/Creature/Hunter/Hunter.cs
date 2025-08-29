using UniRx;
using System;
using Data.Contents;
using System.Collections.Generic;
using UnityEngine;
using Common.State.Hunter;
using Cysharp.Threading.Tasks;
using Common.Skill;

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

    private CharacterController _controller;
    private CameraController _cameraControl;

    private Vector3 _lockDir;
    [SerializeField] private float _curDashTime = 0f;
    
    private void Awake()
    {
        _cameraControl = (Managers.Scene.CurrentScene as GameScene).GetCameraController;
        _controller = GetComponent<CharacterController>();
    }

    public override bool Initialized()
    {
        if (base.Initialized() == false)
            return false;

        BoxCollider collider = _model.GetComponent<BoxCollider>();
        _controller.height = collider.size.y;
        _controller.radius = collider.size.x;
        return true;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (LockGravity == false && _controller.isGrounded == false)
            _controller.Move(Vector3.down * 9.8f * deltaTime);       

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
                _controller.Move(Dir * Speed * deltaTime);
                break;
            case MoveType.Run:
                _controller.Move(Dir * RunSpeed * deltaTime);
                break;
            case MoveType.Dash:
                {
                    _curDashTime += deltaTime;
                    if (_curDashTime >= DashTime)
                    {
                        ChangeState(Define.CreatureState.Idle);
                        return;
                    }

                    _controller.Move(_lockDir.normalized * DashSpeed * deltaTime);
                }
                break;
        }

        Vector3 dir = _moveType != MoveType.Dash ? Dir : _lockDir.normalized;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 10f);
    }


    protected override void UpdateSkill(float deltaTime)
    {
        
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
                            SetAnimation("Walk", 0.05f);
                            break;
                        case MoveType.Run:
                            SetAnimation("Run", 0.05f);
                            break;
                        case MoveType.Dash:
                            SetAnimation("Dash", 0.05f);
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
