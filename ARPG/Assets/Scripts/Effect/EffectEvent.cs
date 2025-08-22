using System;

[Serializable]
public abstract class BaseEffectEvent
{
    public float ActionTime;

    private bool _actionFlag = true;

    public virtual void Action()
    {
        if (_actionFlag == false)
            return;

        _actionFlag = true;
        OnAction();
    }

    public abstract void OnAction();
}

public class ShowObject : BaseEffectEvent
{
    public override void OnAction()
    {
        
    }
}

public class PlaySfx : BaseEffectEvent
{
    public override void OnAction()
    {
        
    }
}