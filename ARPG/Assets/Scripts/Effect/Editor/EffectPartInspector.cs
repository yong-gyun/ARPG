using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectPart))]
public class EffectPartInsepctor : Editor
{
    private SerializedProperty _moveProperty;
    private SerializedProperty _scaleProperty;
    private SerializedProperty _rotationProperty;

    private SerializedProperty _isMoveActive;
    private SerializedProperty _isRotationActive;
    private SerializedProperty _isScaleActive;

    private bool _moveFold;
    private bool _rotationFold;
    private bool _scaleFold;

    private GUIStyle _box;

    private void OnEnable()
    {
        _isMoveActive = serializedObject.FindProperty("_isMoveActive");
        _isRotationActive = serializedObject.FindProperty("_isRotationActive");
        _isScaleActive = serializedObject.FindProperty("_isScaleActive");
        
        _moveProperty = serializedObject.FindProperty("_moveEvent");
        _rotationProperty = serializedObject.FindProperty("_rotationEvent");
        _scaleProperty = serializedObject.FindProperty("_scaleEvent"); 

    }

    public override void OnInspectorGUI()
    {
        _box = new GUIStyle("box") { padding = new RectOffset(8, 8, 8, 8) };

        serializedObject.Update();

        EditorGUILayout.Space(4);

        float width = Mathf.Floor((EditorGUIUtility.currentViewWidth - 30f) / 3f);
        using (new EditorGUILayout.HorizontalScope())
        {
            DrawToggleLeft(_isMoveActive, "Use Move", width);
            DrawToggleLeft(_isRotationActive, "Use Rotate", width);
            DrawToggleLeft(_isScaleActive, "Use Scale", width);
        }

        if (_isMoveActive.boolValue)
        {
            _moveFold = DrawFoldHeader("이동", _moveFold);

            if (_moveFold)
            {
                using (new EditorGUILayout.VerticalScope(_box))
                {
                    DrawChildrenWithoutFoldout(_moveProperty);
                }
            }
        }


        if (_isRotationActive.boolValue)
        {
            _rotationFold = DrawFoldHeader("회전", _rotationFold);

            if (_rotationFold)
            {
                using (new EditorGUILayout.VerticalScope(_box))
                {
                    DrawChildrenWithoutFoldout(_rotationProperty);
                }
            }
        }


        if (_isScaleActive.boolValue)
        {
            _scaleFold = DrawFoldHeader("크기", _scaleFold);

            if (_scaleFold)
            {
                using (new EditorGUILayout.VerticalScope(_box))
                {
                    DrawChildrenWithoutFoldout(_scaleProperty);
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawToggleLeft(SerializedProperty prop, string label, float width)
    {
        prop.boolValue = EditorGUILayout.ToggleLeft(new GUIContent(label), prop.boolValue, GUILayout.Width(width));
    }

    private bool DrawFoldHeader(string title, bool fold)
    {
        var line = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);

        var triRect = new Rect(line.x, line.y, 18f, line.height);
        var labelRect = new Rect(line.x + 18f, line.y, line.width - 18f, line.height);

        bool newFold = EditorGUI.Foldout(triRect, fold, GUIContent.none, true);

        GUIStyle headerStyle = EditorStyles.boldLabel;
        EditorGUI.LabelField(labelRect, title, headerStyle);

        var e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0 && labelRect.Contains(e.mousePosition))
        {
            newFold = !newFold;
            e.Use();
        }

        return newFold;
    }

    private void DrawChildrenWithoutFoldout(SerializedProperty parent)
    {
        var it = parent.Copy();
        var end = it.GetEndProperty();
        bool enterChildren = true;

        while (it.NextVisible(enterChildren) && SerializedProperty.EqualContents(it, end) == false)
        {
            EditorGUILayout.PropertyField(it, true);
            enterChildren = false;
        }
    }

}