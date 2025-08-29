using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Effect))]
public class EffectInspector : Editor
{
    private Effect _target;
    private EventActionBehaviour _behaviour;

    private void OnEnable()
    {
        _target = (Effect)target;
        _behaviour = _target.behaviour;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        OnDrawGUI(_behaviour.events);

        if (GUILayout.Button("Add"))
        {
            EventAction evt = new EventAction();
            evt.Type = Define.EventActionType.None;
            evt.Time = _behaviour.events.Count == 0 ? 0f : _behaviour.events.LastOrDefault().Time;
            _behaviour.events.Add(evt);

            serializedObject.ApplyModifiedProperties();
        }
    }

    private void OnDrawGUI(List<EventAction> events)
    {
        for (int i = 0; i < events.Count; i++)
        {
            var evt = events[i];

            EditorGUILayout.BeginHorizontal();
            {
                string foldoutText = evt.foldout == true ? "¡ä" : "¢¹";
                if (GUILayout.Button(foldoutText, GUIStyle.none, GUILayout.Height(-2.5f), GUILayout.Width(25f), GUILayout.Height(25f)))
                {
                    evt.foldout = !evt.foldout;
                }

                evt.Time = EditorGUILayout.FloatField(evt.Time, GUILayout.Width(75f));
                evt.Type = (Define.EventActionType) EditorGUILayout.EnumPopup(evt.Type, GUILayout.Width(275f));

                if (GUILayout.Button("-", GUILayout.Width(25f)))
                {

                }

                if (GUILayout.Button("¡â", GUILayout.Width(25f)))
                {

                }

                if (GUILayout.Button("¡ä", GUILayout.Width(25f)))
                {

                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
