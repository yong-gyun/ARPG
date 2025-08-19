public abstract class BaseEffect
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