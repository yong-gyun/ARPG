using System.Collections.Generic;
using UnityEngine;

public class EffectComponent : MonoBehaviour
{
    private Creature _owner;
    private int _skillID;

    public void Init(int templateID, Creature owner)
    {
        _owner = owner;
        _skillID = templateID;

        Effect[] effects = gameObject.GetComponentsInChildren<Effect>();
        foreach (Effect effect in effects)
            effect.Init(_skillID, owner);
    }
}
