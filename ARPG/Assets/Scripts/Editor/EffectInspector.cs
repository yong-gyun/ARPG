using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

[CustomEditor(typeof(Effect))]
public class EffectInspector : Editor
{
    private Effect _target;
    private EventActionBehaviour _behaviour;
    private GUIStyle _foldoutStyle = new GUIStyle(EditorStyles.label);

    private void OnEnable()
    {
        _target = (Effect)target;
        _behaviour = _target.behaviour;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        GUILayout.Label("EventActionBehaviour", new GUIStyle(EditorStyles.boldLabel));

        var events = _behaviour.events;
        for (int i = 0; i < events.Count; i++)
        {
            var evt = events[i];

            GUILayout.Space(0.2f);
            EditorGUILayout.BeginHorizontal();
            {
                string foldoutText = evt.foldout == true ? "¡ä" : "¢¹";
                if (GUILayout.Button(foldoutText, _foldoutStyle, GUILayout.Width(15f)))
                {
                    evt.foldout = !evt.foldout;
                }

                evt.Time = EditorGUILayout.FloatField(evt.Time, GUILayout.Width(75f));
                evt.Type = (Define.EventActionType)EditorGUILayout.EnumPopup(evt.Type, GUILayout.Width(275f));

                if (GUILayout.Button("-", GUILayout.Width(25f)))
                {
                    events.Remove(evt);
                }

                if (GUILayout.Button("¡â", GUILayout.Width(25f)))
                {
                    if (events[0] != evt && i > 0)
                    {
                        var t = events[i - 1];
                        events[i - 1] = evt;
                        events[i] = t;
                    }
                }

                if (GUILayout.Button("¡ä", GUILayout.Width(25f)))
                {
                    if (events[events.Count - 1] != evt && i < events.Count)
                    {
                        var t = events[i + 1];
                        events[i + 1] = evt;
                        events[i] = t;
                    }
                }

                if (evt.foldout == true)
                    evt.OnGUI();
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Event"))
        {
            EventAction evt = new EventAction();
            evt.Type = Define.EventActionType.None;
            evt.Time = _behaviour.events.Count == 0 ? 0f : _behaviour.events.LastOrDefault().Time;
            _behaviour.events.Add(evt);
            Undo.RecordObject(target, "Add Event");
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnDrawGUI(List<EventAction> events)
    {
        
    }
}
