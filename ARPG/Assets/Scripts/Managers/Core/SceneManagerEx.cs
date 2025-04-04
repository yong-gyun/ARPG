using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get; private set; }

    public IObservable<float> OnLoadSceneCallback { get { return _onLoadSceneCallback.AsObservable(); } }
    private Subject<float> _onLoadSceneCallback = new Subject<float>();

    public async void LoadSceneAsync(Define.SceneType sceneType)
    {
        //TODO �� �ε�
        AsyncOperationHandle handle = Addressables.LoadSceneAsync(GetScenePath(sceneType));
        
        while (handle.IsDone == false)
        {
            //�� �ε尡 �Ϸ�� ������ ���
            _onLoadSceneCallback.OnNext(handle.PercentComplete);
            await UniTask.Yield();
        }

        _onLoadSceneCallback.OnNext(1f);
        Addressables.Release(handle);
    }

    public void RegisterCurrentScene(BaseScene scene)
    {
        CurrentScene = scene;
    }

    public string GetScenePath(Define.SceneType sceneType)
    {
        string path = ZString.Concat(AssetManager.DIRECTORY_PATH, "/", $"Scenes/{sceneType}.unity");
        return path;
    }
}
