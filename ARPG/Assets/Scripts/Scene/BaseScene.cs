using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Define.SceneType SceneType { get { return _sceneType; } }
    [SerializeField] protected Define.SceneType _sceneType;

    protected bool _initialized = false;

    public virtual bool Initialized()
    {
        if (_initialized == true)
            return false;

        Managers.Scene.RegisterCurrentScene(this);
        return true;
    }
}