using System;
using UniRx;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Common.State.Hunter;

public partial class Hunter : Creature
{
    private float _horizontal;
    private float _vertical;

    private void BindMouseInputEvent()
    {
        Managers.Input.InputMouseEventHandler.Subscribe(info =>
        {
            if (State == Define.CreatureState.Skill ||
                State == Define.CreatureState.Hit ||
                State == Define.CreatureState.Dead)
                return;

            if (info.inputState == Define.InputState.Down)
            {
                if (info.keyID == Define.KeyID.NormalAttack)
                {
                    _currentSkill = Define.SkillType.Combat_Attack_1;
                    ChangeState(Define.CreatureState.Skill);
                }
                else if (info.keyID == Define.KeyID.NormalSkill_1)
                {
                    _currentSkill = Define.SkillType.NormalSkill_1;
                    ChangeState(Define.CreatureState.Skill);
                }
                else if (info.keyID == Define.KeyID.NormalSkill_2)
                {
                    _currentSkill = Define.SkillType.NormalSkill_2;
                    ChangeState(Define.CreatureState.Skill);
                }
                else if (info.keyID == Define.KeyID.UltSkill)
                {
                    _currentSkill = Define.SkillType.UltSkill;
                    ChangeState(Define.CreatureState.Skill);
                }
            }
        }).AddTo(this);
    }

    private void BindKeyInputEvent()
    {
        Managers.Input.InputKeyEventHandler.Subscribe(async info =>
        {
            if (State == Define.CreatureState.Skill || 
                State == Define.CreatureState.Hit || 
                State == Define.CreatureState.Dead)
                return;

            if (info.inputState == Define.InputState.Pressed)
            {
                var delta = (1f / 0.1f) * Time.deltaTime;
                if (info.keyID == Define.KeyID.MoveForward)
                {
                    _vertical = Mathf.Clamp(_vertical + delta, -1f, 1f);
                }
                else if (info.keyID == Define.KeyID.MoveBack)
                {
                    _vertical = Mathf.Clamp(_vertical - delta, -1f, 1f);
                }

                if (info.keyID == Define.KeyID.MoveRight)
                {
                    _horizontal = Mathf.Clamp(_horizontal + delta, -1f, 1f);
                }
                else if (info.keyID == Define.KeyID.MoveLeft)
                {
                    _horizontal = Mathf.Clamp(_horizontal - delta, -1f, 1f);
                }
            }

            if (info.inputState == Define.InputState.Up)
            {
                var delta = (1f / 0.1f) * Time.deltaTime;
                switch (info.keyID)
                {
                    case Define.KeyID.MoveForward:
                    case Define.KeyID.MoveBack:

                        while (_vertical != 0f)
                        {
                            _vertical = Mathf.MoveTowards(_vertical, 0f, delta);
                            await UniTask.Yield();
                        }
                        break;
                    case Define.KeyID.MoveLeft:
                    case Define.KeyID.MoveRight:

                        while (_horizontal != 0f)
                        {
                            _horizontal = Mathf.MoveTowards(_horizontal, 0f, delta);
                            await UniTask.Yield();
                        }
                        break;
                }
            }
        }).AddTo(this);
    }
}
