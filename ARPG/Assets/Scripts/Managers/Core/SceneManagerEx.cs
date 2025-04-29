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
    private AsyncOperationHandle _currentSceneAsyncHandle;
    private Subject<float> _onLoadSceneCallback = new Subject<float>();


    public async void LoadSceneAsync(Define.SceneType sceneType)
    {
        if (_currentSceneAsyncHandle.IsValid() == true)
        {
            await Addressables.UnloadSceneAsync(_currentSceneAsyncHandle);
        }

        //TODO 씬 로드
        AsyncOperationHandle handle = Addressables.LoadSceneAsync(GetScenePath(sceneType));
        
        while (handle.IsDone == false)
        {
            //씬 로드가 완료될 때까지 대기
            _onLoadSceneCallback.OnNext(handle.PercentComplete);
            Debug.Log(handle.PercentComplete);
            await UniTask.Yield();
        }

        _onLoadSceneCallback.OnNext(1f);
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
