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
    private MoveType _moveType;
    private UseAttackType _useAttackType;
    private CharacterController _control;

    public override async UniTask Init(int templateID)
    {
        _stats = gameObject.GetOrAddComponent<HunterStats>();
        _stats.Init(templateID);

        await base.Init(templateID);
    }

    protected override void OnUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Dir = new Vector3(horizontal, 0f, vertical);

        base.OnUpdate();
    }

    protected override void UpdateIdle()
    {
        if (Dir != Vector3.zero)
        {
            ChangeState(Define.CreatureState.MOVE);
        }
    }

    protected override void UpdateMove()
    {
        switch (_moveType)
        {
            case MoveType.Walk:
                _control.Move(Dir * _stats.Speed * Time.deltaTime);
                break;
            case MoveType.Run:
                _control.Move(Dir * _stats.Speed * Time.deltaTime);
                break;
            case MoveType.Dash:
                break;
        }
    }

    public override void ChangeState(Define.CreatureState state)
    {

    }
}
