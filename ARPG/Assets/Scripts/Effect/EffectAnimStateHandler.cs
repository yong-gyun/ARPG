using UniRx;
using System;
using UnityEngine;

public class EffectAnimStateHandler : StateMachineBehaviour
{
    private Action _onStateEnterCallback = null;
    private Action<float> _onStateUpdateCallback = null;
    private Action _onStateExitCallback = null;

    public void Initialized(Action onStateEnterCallback, Action<float> onStateUpdateCallback, Action onStateExitCallback)
    {
        _onStateEnterCallback -= onStateEnterCallback;
        _onStateEnterCallback += onStateEnterCallback;

        _onStateUpdateCallback -= onStateUpdateCallback;
        _onStateUpdateCallback += onStateUpdateCallback;

        _onStateExitCallback -= onStateExitCallback;
        _onStateExitCallback += onStateExitCallback;
    }

    public void Clear()
    {
        _onStateEnterCallback = null;
        _onStateUpdateCallback = null;
        _onStateExitCallback = null;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _onStateEnterCallback.Invoke();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float tick = stateInfo.normalizedTime * stateInfo.length;
        _onStateUpdateCallback.Invoke(tick);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _onStateExitCallback.Invoke();
    }
}