using DG.Tweening;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillSettingTool : EditorWindow
{
    public float AnimLength
    {
        get
        {
            if (_animClip == null)
                return 1f;

            return _animClip.length;
        }
    }

    public float PlayTime
    {
        get
        {
            return _playTime;
        }
        set
        {
            float destTime = AnimLength;
            _playTime = Mathf.Clamp(value, 0f, destTime);
        }
    }

    private const float SPACE_INTERVAL = 2.5f;
    private float _playTime = 0f;       //시간 단위로 봄
    private float _playSpeed = 1f;      //배속
    private float _fieldOfView = 30f;
    private double _lastUpdateTick = 0d;
    private double _lastUpdateTime = 0d;

    private string _functionName = "SkillAction";
    private int _command = 0;

    private bool _isDrag = false;
    private bool _hasFocus = false;
    private bool _isPlaying = false;

    private GameObject _previewObject;
    private GameObject _skillEffectPreviewObject;
    private GameObject _effectObject;
    private GameObject _prefab;

    private PreviewRenderUtility _previewUtility;
    private AnimationClip _animClip;
    private EditorEffectPreviewHandler _handler;
    private SkillSettingData _skillSettingData;
    private List<AnimationEvent> _eventCache = new List<AnimationEvent>();

    private const float EDITOR_WIDTH = 1200f;
    private const float EDITOR_HEIGHT = 800f;

    private const float PREVIEW_WIDTH = 900f;
    private const float PREVIEW_HEIGHT = 500f;

    private bool _effectAction;
    private double _effectACtionTime;

    private Vector3 _scrollviewPos;

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
        if (_previewObject != null)
            DestroyImmediate(_previewObject);

        if (_effectObject != null)
            DestroyImmediate(_effectObject);

        _previewUtility.Cleanup();
        _previewObject = null;
    }

    private void Update()
    {
        double now = EditorApplication.timeSinceStartup;
        float delta = (float)(now - _lastUpdateTime);
        _lastUpdateTime = now;

        if (_isPlaying == true)
        {
            PlayTime += delta * _playSpeed;
            if (PlayTime >= AnimLength)
            {
                PlayTime = AnimLength;
                _isPlaying = false;
            }

            Repaint();
        }

        double tick = now - _lastUpdateTick;
        if (tick >= 0.001f)
        {
            _lastUpdateTick = now;
            UpdateTick();
        }
    }

    private void UpdateTick()
    {
        foreach (var mit in _eventCache)
        {
            if (Mathf.Abs(mit.time - _playTime) <= 0.001f)
            {
                _handler.SkillAction(mit.intParameter);
                Debug.Log("SkillAction");
            }
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            //프리뷰 및 옵션 설정
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical("box", GUILayout.Width(300), GUILayout.Height(500));
                {
                    DrawPreviewer(PREVIEW_WIDTH, PREVIEW_HEIGHT);
                    DrawEvents();
                }
                EditorGUILayout.EndVertical();

                DrawHierachy();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                ShowInputDatas();
                AddAnimationEvent();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawHierachy()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(150), GUILayout.Height(510));
        {
            EditorGUILayout.BeginScrollView(_scrollviewPos, GUILayout.Width(900), GUILayout.Height(500));
            {
                
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawInspector()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(150), GUILayout.Height(510));
        {
            EditorGUILayout.BeginScrollView(_scrollviewPos, GUILayout.Width(900), GUILayout.Height(500));
            {

            }
            EditorGUILayout.EndScrollView();
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
            previewCam.transform.position = center - Vector3.forward * distance + Vector3.up * 0.5f;
            
            if (_animClip != null)
                _animClip.SampleAnimation(_previewObject, PlayTime);

            _previewUtility.Render();
            texture = _previewUtility.EndPreview();
        }

        GUILayout.Box(texture, GUILayout.Width(900), GUILayout.Height(475));
    }

    private void DrawEvents()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.Width(PREVIEW_WIDTH));
        {
            if (GUILayout.Button(_isPlaying == true ? "II" : "▷", GUILayout.Width(25)) == true)
            {
                if (_isPlaying == false)
                {
                    if (PlayTime >= AnimLength)
                        PlayTime = 0f;
                }

                _isPlaying = !_isPlaying;
            }

            PlayTime = EditorGUILayout.FloatField(PlayTime, GUILayout.Width(50));
            PlayTime = GUILayout.HorizontalSlider(PlayTime, 0f, AnimLength, GUILayout.Width(690));
            GUILayout.Space(SPACE_INTERVAL);

            _playSpeed = GUILayout.HorizontalSlider(_playSpeed, 0.1f, 2f, GUILayout.Width(50));
            GUILayout.Label($"{_playSpeed:F2}x", GUILayout.Width(40));
        }
        EditorGUILayout.EndHorizontal();
    }

    private void AddAnimationEvent()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(400));
        {
            _functionName = EditorGUILayout.TextField(_functionName, GUILayout.Width(400));

            EditorGUILayout.Space(SPACE_INTERVAL);

            _command = EditorGUILayout.IntField("Command", _command, GUILayout.Width(400));
            EditorGUILayout.Space(SPACE_INTERVAL);

            if (GUILayout.Button("이벤트 생성", GUILayout.Width(400)) == true)
            {
                AnimationEvent animEvent = new AnimationEvent();
                animEvent.time = PlayTime;
                animEvent.functionName = _functionName;
                animEvent.intParameter = _command;

                AnimationUtility.SetAnimationEvents(_animClip, new AnimationEvent[] { animEvent });
                UpdateAnimEvent();
            }

            if (GUILayout.Button("새로고침", GUILayout.Width(400)) == true)
            {
                UpdateAnimEvent();
            }
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
                if (_previewObject != null)
                    DestroyImmediate(_previewObject);

                _previewObject = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
                _previewUtility.camera.transform.position = new Vector3(0, 0, -10);
                _previewUtility.AddSingleGO(_previewObject);

                _handler = _previewObject.GetOrAddComponent<EditorEffectPreviewHandler>();
                _handler.Init(_skillSettingData);
                _effectObject = _handler.Effect.gameObject;

                var renderers = _previewObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                    _rendererBuffer.Add(renderer);

                UpdateAnimEvent();
            }
        }
        EditorGUILayout.EndVertical();
    }

    private void UpdateAnimEvent()
    {
        if (_animClip != null)
        {
            _eventCache.Clear();

            var events = AnimationUtility.GetAnimationEvents(_animClip);
            foreach (var mit in events)
                _eventCache.Add(mit);
        }
    }
}
