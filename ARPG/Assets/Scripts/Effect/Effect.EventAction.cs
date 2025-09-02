using UnityEngine;

public partial class Effect : BaseObject
{
    public void SetRunner(EventActionRunner runner)
    {
        _runner = new EventActionRunner();
        _runner.AddEventAction(Define.EventActionType.Bound, OnEventBound);
        _runner.AddEventAction(Define.EventActionType.BoundHeal, OnEventBoundHeal);
        _runner.AddEventAction(Define.EventActionType.BoundDamage, OnEventBoundDamage);
        _runner.AddEventAction(Define.EventActionType.BoundBuff, OnEventBoundBuff);
        _runner.AddEventAction(Define.EventActionType.BoundDebuff, OnEventBoundDebuff);
        _runner.AddEventAction(Define.EventActionType.SetAnimation, OnEventSetAnimation);
        _runner.AddEventAction(Define.EventActionType.Position, OnEventPosition);
        _runner.AddEventAction(Define.EventActionType.InputAction, OnEventInputAction);
        _runner.AddEventAction(Define.EventActionType.Effect, OnEventEffect);
    }

    #region Event Collision Methods
    public void OnEventBound(EventAction eventAction)
    {
        Debug.Log("OnEventBound");
    }

    public void OnEventBoundHeal(EventAction eventAction)
    {
        Debug.Log("OnEventBoundHeal");

    }

    public void OnEventBoundDamage(EventAction eventAction)
    {

        Debug.Log("OnEventBoundDamage");
    }

    public void OnEventBoundBuff(EventAction eventAction)
    {

        Debug.Log("OnEventBoundBuff");
    }

    public void OnEventBoundDebuff(EventAction eventAction)
    {

        Debug.Log("OnEventBoundDebuff");
    }
    #endregion

    #region Event Methods
    public void OnEventSetAnimation(EventAction eventAction)
    {
        Define.CreatureType creatureType = (Define.CreatureType)eventAction.ints[0];
        string animationName = eventAction.strings[0];
        float duration = eventAction.floats[0];
        var target = creatureType == Define.CreatureType.Hunter ? Owner : Target;
        target.SetAnimation(animationName, duration);
    }

    public void OnEventPosition(EventAction eventAction)
    {

    }

    public void OnEventInputAction(EventAction eventAction)
    {

    }

    public void OnEventEffect(EventAction eventAction)
    {

    }
    #endregion
}
