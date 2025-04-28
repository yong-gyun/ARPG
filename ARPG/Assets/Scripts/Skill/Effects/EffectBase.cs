using Data.Contents;
using UnityEngine;

[System.Serializable]
public abstract class EffectBase
{
    protected Effect _effect;

    public virtual void Init(Effect effect) 
    { 
        _effect = effect;
    }

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Destory() { }
}