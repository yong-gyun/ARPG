// TypeSelectPopup.cs (Editor/)
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class SelectTypePopup : PopupWindowContent
{
    private Action<Type> _onSelectedTypeEvent = null;
    private Type _baseType;
    private List<Type> _selectableTypes = new List<Type>();

    private Vector2 _scrollPos;
    private readonly Color _hoverColor = new Color(0.24f, 0.49f, 0.90f, 0.15f);


    public SelectTypePopup(Type baseType, Action<Type> onSelectedTypeEvent)
    {
        _baseType = baseType;
        _onSelectedTypeEvent = onSelectedTypeEvent;
    }

    public override void OnOpen()
    {
        _selectableTypes = TypeCache.GetTypesDerivedFrom(_baseType).ToList();
    }

    public override void OnGUI(Rect rect)
    {
        EditorGUILayout.BeginScrollView(_scrollPos);
        {
            foreach (var type in _selectableTypes)
                DrawTypeRow(type);
        }
        EditorGUILayout.EndScrollView();
    }

    public void DrawTypeRow(Type type)
    {
        if (GUILayout.Button(type.Name, EditorStyles.label))
        {
            _onSelectedTypeEvent.Invoke(type);
            editorWindow.Close();
        }
    }
}
#endif
