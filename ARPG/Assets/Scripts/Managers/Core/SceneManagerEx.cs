using UnityEngine;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get; private set; }

    public void LoadScene(Define.SceneType sceneType)
    {
        //TODO �� �ε�
    }

    public void RegisterCurrentScene(BaseScene scene)
    {
        CurrentScene = scene;
    }
}
