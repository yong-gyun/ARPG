using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class ButtonAttributeInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var targets = this.targets;
        foreach (var target  in targets)
        {
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var m in methods)
            {
                var attribute = m.GetCustomAttribute<ButtonAttribute>();
                if (attribute == null) 
                    continue;

                if (m.GetParameters().Length > 0)
                    continue;

                var label = string.IsNullOrEmpty(attribute.lable) ? m.Name : attribute.lable;
                if (GUILayout.Button(label))
                {
                    Undo.RecordObject(target, label);
                    m.Invoke(target, null);
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}
