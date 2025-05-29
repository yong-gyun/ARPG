using UnityEditor;
using UnityEngine;


public class SkillSettingTool : EditorWindow
{
    PreviewRenderUtility _previewUtility;
    GameObject _previewObject;
    GameObject _prefab;
    AnimationClip _animClip;
    SkillSettingData _skillSettingData;

    [MenuItem("Tools/��ų ���� ��")]
    static void OpenWindow()
    {
        GetWindow<SkillSettingTool>("��ų ���� ��");
    }

    private void OnEnable()
    {
        _previewUtility = new PreviewRenderUtility();
        
    }

    private void OnDisable()
    {
        _previewUtility.Cleanup();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            //������ �� �ɼ� ����
            EditorGUILayout.BeginHorizontal();
            {
                //��� ��
                EditorGUILayout.BeginHorizontal(GUILayout.Width(800), GUILayout.Height(800));
                {
                    if (_prefab != null && _animClip != null)
                    {

                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical(GUILayout.Width(400));
                {
                    Rect rect = GUILayoutUtility.GetRect(200, EditorGUIUtility.singleLineHeight);
                    _prefab = EditorGUILayout.ObjectField("������", _prefab, typeof(GameObject), false) as GameObject;
                    _animClip = EditorGUILayout.ObjectField("�ִϸ��̼� Ŭ��", _animClip, typeof(AnimationClip), false) as AnimationClip;
                    _skillSettingData = EditorGUILayout.ObjectField("���� ������", _prefab, typeof(SkillSettingData), false) as SkillSettingData;

                    if (GUILayout.Button("����") == true)
                    {

                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            

            
        }
        EditorGUILayout.EndVertical();
    }
}
