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
                    if (_currentSkill == Define.SkillType.None || CheckNotNormalAttack(_currentSkill) == false)
                    {
                        //이전에 사용한 스킬이 기본 공격이 아니였으니 콤보 어택1로 설정
                        _currentSkill = Define.SkillType.Combat_Attack_1;
                    }
                    else
                    {
                        //이전에 콤보 어택을 사용했으니 다음 콤보 어택으로 설정
                        switch (_currentSkill)
                        {
                            case Define.SkillType.Combat_Attack_1:
                                _currentSkill = Define.SkillType.Combat_Attack_2;
                                break;
                            case Define.SkillType.Combat_Attack_2:
                                _currentSkill = Define.SkillType.Combat_Attack_3;
                                break;
                            case Define.SkillType.Combat_Attack_3:
                                _currentSkill = Define.SkillType.Combat_Attack_4;
                                break;
                            default:
                                _currentSkill = Define.SkillType.None;  //Idle로 전환
                                break;
                        }
                    }

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

    private bool CheckNotNormalAttack(Define.SkillType skillType)
    {
        if (skillType == Define.SkillType.Combat_Attack_1 ||
            skillType == Define.SkillType.Combat_Attack_2 ||
            skillType == Define.SkillType.Combat_Attack_3 ||
            skillType == Define.SkillType.Combat_Attack_4)
            return true;

        return false;
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
