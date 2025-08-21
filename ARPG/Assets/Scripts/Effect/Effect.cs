using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeReference, SubclassSelector] public List<BaseEffectEvent> Effects = new List<BaseEffectEvent>();

    public void UpdateTick(float tick)
    {
        foreach (BaseEffectEvent effect in Effects)
        {
            if (effect.ActionTime >= tick)
                effect.Action();
        }
    }
}