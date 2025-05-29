using UnityEditor;
using UnityEngine;


public class SkillSettingTool : EditorWindow
{
    PreviewRenderUtility _previewUtility;
    GameObject _previewObject;
    GameObject _prefab;
    AnimationClip _animClip;
    SkillSettingData _skillSettingData;

    [MenuItem("Tools/스킬 세팅 툴")]
    static void OpenWindow()
    {
        GetWindow<SkillSettingTool>("스킬 세팅 툴");
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
            //프리뷰 및 옵션 설정
            EditorGUILayout.BeginHorizontal();
            {
                //재생 바
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
                    _prefab = EditorGUILayout.ObjectField("프리팹", _prefab, typeof(GameObject), false) as GameObject;
                    _animClip = EditorGUILayout.ObjectField("애니메이션 클립", _animClip, typeof(AnimationClip), false) as AnimationClip;
                    _skillSettingData = EditorGUILayout.ObjectField("세팅 데이터", _prefab, typeof(SkillSettingData), false) as SkillSettingData;

                    if (GUILayout.Button("생성") == true)
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
