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

    }

    public void OnEventBoundHeal(EventAction eventAction)
    {

    }

    public void OnEventBoundDamage(EventAction eventAction)
    {

    }

    public void OnEventBoundBuff(EventAction eventAction)
    {

    }

    public void OnEventBoundDebuff(EventAction eventAction)
    {

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
