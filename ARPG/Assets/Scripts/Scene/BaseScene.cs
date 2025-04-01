using UnityEngine;

public class BaseScene : InitBase
{
    public Define.SceneType SceneType { get { return _sceneType; } }
    [SerializeField] protected Define.SceneType _sceneType;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Scene.RegisterCurrentScene(this);
        return true;
    }
}