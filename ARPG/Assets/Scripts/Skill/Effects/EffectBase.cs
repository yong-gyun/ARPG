using UniRx;
using Data.Contents;
using System.Collections.Generic;
using System.Collections.Generic.Serialized;
using UnityEngine;
using System.Linq;

[System.Serializable]
public abstract class EffectBase
{
    protected Effect _parent;

    public virtual void Init(Effect parent) 
    { 
        _parent = parent;
    }

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Destory() { }
}

