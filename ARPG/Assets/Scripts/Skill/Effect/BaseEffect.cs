using System;
using UnityEngine;

[Serializable]
public abstract class BaseEffect
{
    public Creature Owner { get; protected set; }

    public int command;

    public virtual void Init(Creature owner) { Owner = owner; }

    public abstract void PlayAction();
}