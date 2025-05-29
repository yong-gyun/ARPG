using DG.Tweening;
using UnityEditor;
using UnityEngine;


public class SkillSettingTool : EditorWindow
{
    private const float SPACE_INTERVAL = 2.5f;
    private const float FRAME_RATE = 60f;
    private float _playFrame = 0f;
    private float _playSpeed = 1f;
    private bool _hasFocus = false;
    private bool _isPlaying = false;

    private PreviewRenderUtility _previewUtility;
    private GameObject _previewObject;
    private GameObject _skillEffectPreviewObject;
    private GameObject _prefab;
    private AnimationClip _animClip;
    private SkillSettingData _skillSettingData;

    [MenuItem("Tools/스킬 세팅 툴")]
    static void OpenWindow()
    {
        var window = GetWindow<SkillSettingTool>("스킬 세팅 툴");
        window.minSize = new Vector2(1200, 500);
    }

    private void OnFocus()
    {
        _hasFocus = true;
    }

    private void OnLostFocus()
    {
        _hasFocus = false;
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
                EditorGUILayout.BeginVertical("box", GUILayout.Width(900), GUILayout.Height(500));
                {
                    Texture result = null;

                    if (_previewObject != null)
                    {
                        Rect previewRect = new Rect();
                        previewRect.width = 900;
                        previewRect.height = 475;

                        _previewUtility.BeginPreview(previewRect, GUIStyle.none);

                        _previewUtility.camera.transform.LookAt(_previewObject.transform);
                        _previewUtility.camera.fieldOfView = 30f;

                        _previewUtility.Render();
                        result = _previewUtility.EndPreview();
                    }

                    GUILayout.Box(result, GUILayout.Width(900), GUILayout.Height(475));
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button(_isPlaying == true ? "II" : "▷", GUILayout.Width(25)) == true)
                        {
                            
                        }

                        GUILayout.Label($"{_playFrame:F2}", GUILayout.Width(30));

                        _playFrame = GUILayout.HorizontalSlider(_playFrame, 0f, _animClip != null ? _animClip.length : 1f, GUILayout.Width(690));
                        GUILayout.Space(SPACE_INTERVAL);

                        _playSpeed = GUILayout.HorizontalSlider(_playSpeed, 0.1f, 2f, GUILayout.Width(100));
                        GUILayout.Label($"{_playSpeed:F2}x", GUILayout.Width(40));
                    }
                    EditorGUILayout.EndHorizontal();

                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();


            ShowInputDatas();
        }
        EditorGUILayout.EndVertical();
    }

    private void ShowInputDatas()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(400));
        {
            _prefab = EditorGUILayout.ObjectField("프리팹", _prefab, typeof(GameObject), false) as GameObject;
            EditorGUILayout.Space(SPACE_INTERVAL);

            _animClip = EditorGUILayout.ObjectField("애니메이션 클립", _animClip, typeof(AnimationClip), false) as AnimationClip;
            EditorGUILayout.Space(SPACE_INTERVAL);

            _skillSettingData = EditorGUILayout.ObjectField("세팅 데이터", _skillSettingData, typeof(SkillSettingData), false) as SkillSettingData;
            EditorGUILayout.Space(SPACE_INTERVAL);

            if (GUILayout.Button("생성") == true)
            {
                _previewObject = GameObject.Instantiate(_prefab);
                _previewUtility.camera.transform.position = new Vector3(0, 0, -10);
                _previewUtility.AddSingleGO(_previewObject);
            }
        }
        EditorGUILayout.EndVertical();
    }
}
