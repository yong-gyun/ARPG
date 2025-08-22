using System;
using UnityEngine;

public class SkillActionHandler : MonoBehaviour
{
    private Action _onEnterSkillCallback;
    private Action _onUpdateSkillCallback;
    private Action _onExitSkillCallback;

    public void SetSkillAction(Action onEnterSkillCallback, Action onUpdateSkillCallback, Action onExitSkillCallback)
    {
        _onEnterSkillCallback = onEnterSkillCallback;
        _onUpdateSkillCallback = onUpdateSkillCallback;
        _onExitSkillCallback = onExitSkillCallback;
    }


}