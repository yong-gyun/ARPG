using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public List<BaseEffect> Effects = new List<BaseEffect>();

    public void UpdateTick(float tick)
    {
        foreach (BaseEffect effect in Effects)
        {
            if (effect.ActionTime >= tick)
                effect.Action();
        }
    }
}

public class EffectInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("테스트 플레이"))
        {

        }
    }
}