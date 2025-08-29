using UnityEngine;

public partial class Effect : BaseObject
{
    public void SetRunner(EventActionRunner runner)
    {
        _runner = new EventActionRunner();
        _runner.AddEventAction(Define.EventActionType.Collision, OnEventCollision);
        _runner.AddEventAction(Define.EventActionType.CollisionHeal, OnEventCollisionHeal);
        _runner.AddEventAction(Define.EventActionType.CollisionDamage, OnEventCollisionDamage);
        _runner.AddEventAction(Define.EventActionType.CollisionBuff, OnEventCollisionBuff);
        _runner.AddEventAction(Define.EventActionType.CollisionDebuff, OnEventCollisionDebuff);
        _runner.AddEventAction(Define.EventActionType.SetAnimation, OnEventSetAnimation);
        _runner.AddEventAction(Define.EventActionType.Position, OnEventPosition);
        _runner.AddEventAction(Define.EventActionType.InputAction, OnEventInputAction);
        _runner.AddEventAction(Define.EventActionType.Effect, OnEventEffect);
    }

    #region Event Collision Methods
    public void OnEventCollision(EventAction eventAction)
    {

    }

    public void OnEventCollisionHeal(EventAction eventAction)
    {

    }

    public void OnEventCollisionDamage(EventAction eventAction)
    {

    }

    public void OnEventCollisionBuff(EventAction eventAction)
    {

    }

    public void OnEventCollisionDebuff(EventAction eventAction)
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
