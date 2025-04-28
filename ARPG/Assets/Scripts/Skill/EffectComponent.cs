using System.Collections.Generic;
using UnityEngine;

public class EffectComponent : MonoBehaviour
{
    private int _skillID;

    public void Init(int templateID)
    {
        _skillID = templateID;

        Effect[] effects = gameObject.GetComponentsInChildren<Effect>();
        foreach (Effect effect in effects)
            effect.Init(_skillID);
    }
}
