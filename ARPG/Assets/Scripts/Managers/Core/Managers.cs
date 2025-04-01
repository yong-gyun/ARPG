using UnityEngine;

public class Managers : MonoBehaviour
{
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
    public static PopupManager Popup { get { return Instance._popup; } }

    private ResourceManager _resource = new ResourceManager();
    private SoundManager _sound = new SoundManager();
    private DataManager _data = new DataManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private PopupManager _popup = new PopupManager();
    
    static void Init()
    {

    }
}
