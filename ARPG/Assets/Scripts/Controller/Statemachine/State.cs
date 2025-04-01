using UnityEngine;

public abstract class State<T>
{
    public StateMachine<T> Owner { get; private set; }
    public T Creature { get; private set; }

    public void Init(StateMachine<T> owner, T creature)
    {
        Owner = owner;
        Creature = creature;

        Init();
    }

    protected virtual void Init() { }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
