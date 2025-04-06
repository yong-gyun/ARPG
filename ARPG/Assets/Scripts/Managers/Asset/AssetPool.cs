using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class AssetPool
{
    public class AssetRefData
    {
        public int count;

        private string _key;
        private AsyncOperationHandle _handle;
        private Object _resourceCache;

        public AssetRefData(string key, AsyncOperationHandle handle)
        {
            this._key = key;
            _handle = handle;
            _resourceCache = handle.Result as Object;
        }

        public Object UseAsset()
        {
            Interlocked.Increment(ref count);
            Debug.Log($"에셋 사용: {_key}, {count}");
            return _resourceCache;
        }

        public void Release()
        {
            Addressables.Release(_handle);
        }
    }

    private Dictionary<string, AssetRefData> _addressableRefDatas = new Dictionary<string, AssetRefData>();
    private HashSet<string> _releaseAssets = new HashSet<string>();
    
    public async UniTask<T> LoadAsync<T>(string path) where T : Object
    {
        try
        {
            if (_addressableRefDatas.ContainsKey(path) == false)
            {
                var handle = Addressables.LoadAssetAsync<T>(path);
                await UniTask.WaitUntil(() => handle.IsDone == true);

                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError($"리소스 로드 실패 : {path}");
                    return null;
                }

                AssetRefData refData = new AssetRefData(path, handle);
                Debug.Log($"리소스 로드 성공: {path}");

                _addressableRefDatas[path] = refData;
            }
        }
        catch (Exception e)
        {

        }

        return _addressableRefDatas[path].UseAsset() as T;
    }

    public void Release(string key, int releaseCount, bool releaseImmediate = false)
    {
        int ms = releaseImmediate == true ? 0 : TimeSpan.FromSeconds(60).Milliseconds;
        if (_addressableRefDatas.TryGetValue(key, out AssetRefData refData) == true)
        {
            if (Interlocked.Add(ref refData.count, -releaseCount) <= 0)
                ReleaseRefData(key, ms).Forget();
        }
    }

    public async UniTaskVoid ReleaseRefData(string key, int delayMs)
    {
        //이미 릴리즈 대상이라는 뜻
        if (_releaseAssets.Contains(key) == true)
            return;

        _releaseAssets.Add(key);
        int elapsed = 0;
        int interval = 100;     //0.1초마다 확인
        var refData = _addressableRefDatas[key];

        while (elapsed < delayMs)
        {
            await UniTask.Delay(interval);
            elapsed += interval;

            if (refData.count > 0)
            {
                //릴리즈 취소
                Debug.Log($"릴리즈 취소: {key}, {refData.count}");
                return;
            }
        }

        if (refData.count <= 0)
        {
            //아무 곳에서도 사용하지 않음
            _addressableRefDatas.Remove(key);
            refData.Release();
            Debug.Log($"에셋 릴리즈 {key}");
        }

        _releaseAssets.Remove(key);
    }
}