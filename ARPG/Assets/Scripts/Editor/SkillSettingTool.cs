using DG.Tweening;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SkillSettingTool : EditorWindow
{
    private const float SPACE_INTERVAL = 2.5f;
    private float _playTime = 0f;       //시간 단위로 봄
    private float _playSpeed = 1f;      //배속
    private float _fieldOfView = 30f;

    private bool _isDrag = false;
    private bool _hasFocus = false;
    private bool _isPlaying = false;

    private PreviewRenderUtility _previewUtility;
    private GameObject _previewObject;
    private GameObject _skillEffectPreviewObject;
    private GameObject _prefab;
    private AnimationClip _animClip;
    private SkillSettingData _skillSettingData;

    private const float EDITOR_WIDTH = 1200f;
    private const float EDITOR_HEIGHT = 800f;

    private const float PREVIEW_WIDTH = 900f;
    private const float PREVIEW_HEIGHT = 500f;

    private List<Renderer> _rendererBuffer = new List<Renderer>();

    [MenuItem("Tools/스킬 세팅 툴")]
    static void OpenWindow()
    {
        var window = GetWindow<SkillSettingTool>("스킬 세팅 툴");
        window.minSize = new Vector2(EDITOR_WIDTH, EDITOR_HEIGHT);
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
                    DrawPreviewer(PREVIEW_WIDTH, PREVIEW_HEIGHT);
                    DrawEvents();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            ShowInputDatas();
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawPreviewer(float width, float height)
    {
        Texture texture = null;
        if (_previewObject != null)
        {
            Rect previewRect = new Rect();
            previewRect.width = width;
            previewRect.height = height;

            _previewUtility.BeginPreview(previewRect, GUIStyle.none);
            _previewUtility.camera.fieldOfView = _fieldOfView;

            Bounds bounds = new Bounds(_previewObject.transform.position, Vector3.zero);
            foreach (Renderer renderer in _rendererBuffer)
                bounds.Encapsulate(renderer.bounds);

            Vector3 center = bounds.center;
            float size = bounds.size.magnitude;
            float distance = size / (2.0f * Mathf.Tan(0.5f * _fieldOfView * Mathf.Deg2Rad));

            Camera previewCam = _previewUtility.camera;
            previewCam.transform.position = center - Vector3.forward * distance;
            previewCam.transform.LookAt(_previewObject.transform);

            if (_animClip != null)
                _animClip.SampleAnimation(_previewObject, _playTime);

            _previewUtility.Render();
            Texture result = _previewUtility.EndPreview();
        }

        GUILayout.Box(texture, GUILayout.Width(900), GUILayout.Height(475));
    }

    private void DrawEvents()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.Width(PREVIEW_WIDTH));
        {
            if (GUILayout.Button(_isPlaying == true ? "II" : "▷", GUILayout.Width(25)) == true)
            {
                _isPlaying = !_isPlaying;
            }

            _playTime = EditorGUILayout.FloatField(_playTime, GUILayout.Width(30));
            _playTime = GUILayout.HorizontalSlider(_playTime, 0f, _animClip != null ? _animClip.length : 1f, GUILayout.Width(690));
            GUILayout.Space(SPACE_INTERVAL);

            _playSpeed = GUILayout.HorizontalSlider(_playSpeed, 0.1f, 2f, GUILayout.Width(50));
            GUILayout.Label($"{_playSpeed:F2}x", GUILayout.Width(40));
        }
        EditorGUILayout.EndHorizontal();
    }

    private void AddAnimationEvent()
    {

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

                var renderers = _previewObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                    _rendererBuffer.Add(renderer);
            }
        }
        EditorGUILayout.EndVertical();
    }
}
