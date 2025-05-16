using UnityEngine;

public class Managers : MonoBehaviour
{
    public bool OnInitialized { get { return _init; } }

    private static bool _init;

    public static Managers Instance
    {
        get
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                    go = new GameObject("@Managers");

                s_instance = go.GetOrAddComponent<Managers>();
                DontDestroyOnLoad(go);
                Init();   
            }

            return s_instance;
        }
    }

    static Managers s_instance;

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static DataManager Data { get { return Instance._data; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }

    private ResourceManager _resource = new ResourceManager();
    private SoundManager _sound = new SoundManager();
    private DataManager _data = new DataManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();
    private ObjectManager _object = new ObjectManager();
    private InputManager _input = new InputManager();
    private PoolManager _pool = new PoolManager();

    static void Init()
    {
        s_instance._input.Init();
        _init = true;
    }

    private void Update()
    {
        if (OnInitialized == false)
            return;

        _input.OnUpdate();
    }

    private void OnApplicationQuit()
    {
        _input.SaveData();
    }
}
