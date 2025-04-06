using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StartScene : BaseScene
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        var handle = Addressables.InitializeAsync();
        handle.Completed += OnCompletedInitAddressables;
        return true;
    }

    private void OnCompletedInitAddressables(AsyncOperationHandle<IResourceLocator> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            double size = GetDownloadResourceSize(AssetManager.DOWNLOAD_LABEL);
            if (size > 0)
            {
                //�ٿ�ε� ����
                StartDownload();
            }
            else
            {
                //��ġ�� ���� ����
                OnCompletedResourceDownload();
            }
        }
    }

    private async void StartDownload()
    {
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(AssetManager.DOWNLOAD_LABEL);
        while (handle.GetDownloadStatus().IsDone == false)
        {
            Debug.Log($"���ҽ� �ٿ�ε� �� {handle.GetDownloadStatus().DownloadedBytes}mb / {handle.GetDownloadStatus().TotalBytes}mb");
            await UniTask.Delay(100);
        }

        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.Log("���ҽ� �ٿ�ε� ����");
            return;
        }

        Addressables.Release(handle);

        OnCompletedResourceDownload();
    }

    public async void OnCompletedResourceDownload()
    {
        await Managers.Data.LoadAll();
        Managers.Scene.LoadSceneAsync(Define.SceneType.GameScene);
    }

    private double GetDownloadResourceSize(string label)
    {
#if UNITY_EDITOR
        //��巹���� ���Ӽ� ĳ�� ����
        Addressables.ClearDependencyCacheAsync(label);
#endif
        double size = 0d;
        AsyncOperationHandle<long> handle = Addressables.GetDownloadSizeAsync(label);
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            size += Convert.ToDouble(handle.Result);
        }
        else
        {
            Debug.Log("Failed Get Download Size");
        }

        Addressables.Release(handle);
        Debug.Log($"Update Resource Size {size}MB");
        return size;
    }
}
