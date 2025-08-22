using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeReference, SubclassSelector] public List<BaseEffectEvent> Effects = new List<BaseEffectEvent>();

    public void Initialized(Creature owner)
    {
        EffectAnimStateHandler handler = owner.GetComponent<EffectAnimStateHandler>();
        handler.Initialized(OnEnter, OnUpdate, OnExit);
    }

    public void OnEnter()
    {

    }

    public void OnUpdate(float tick)
    {
        foreach (BaseEffectEvent effect in Effects)
        {
            if (effect.ActionTime >= tick)
                effect.Action();
        }
    }

    public void OnExit()
    {

    }
}