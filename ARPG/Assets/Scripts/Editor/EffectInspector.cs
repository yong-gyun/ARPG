#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Effect))]
public class EffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var addRect = GUILayoutUtility.GetRect(120, 20);
        if (GUI.Button(addRect, "Add"))
        {
            SelectTypePopup popup = new SelectTypePopup(typeof(BaseEffectEvent), CreateEffectInstance);
            PopupWindow.Show(addRect, popup);
        }
    }

    private void CreateEffectInstance(Type type)
    {
        object instance = Activator.CreateInstance(type);
        Effect effect = (Effect)target;
        
        var effects = effect.Effects;
        BaseEffectEvent be = (BaseEffectEvent) instance;

        if (effects.Count != 0)
        {
            var lastEffect = effects.LastOrDefault();
            be.ActionTime = lastEffect.ActionTime;
        }

        effect.Effects.Add(be);
    }
}
#endif
