using System;
using UnityEngine;

[Serializable]
public abstract class BaseEffect
{
    public GameObject Owner { get; protected set; }

    public int command;

    public virtual void Init(GameObject owner) { Owner = owner; }

    public abstract void PlayAction();
}