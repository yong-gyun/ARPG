using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class AssetPool
{
    public class AssetRefData
    {
        public string Key { get { return _key; } }
        public int count;
        public Object GetAssetOrigin { get { return _resource; } }

        private string _key;
        private AsyncOperationHandle _handle;
        private Object _resource;

        public AssetRefData(string key, AsyncOperationHandle handle)
        {
            this._key = key;
            _handle = handle;
            _resource = handle.Result as Object;
        }

        public Object UseAsset()
        {
            Interlocked.Increment(ref count);
            Debug.Log($"���� ���: {_key}, {count}");
            return _resource;
        }

        public void Release()
        {
            Addressables.Release(_handle);
        }
    }

    private Dictionary<string, AssetRefData> _addressableRefDatas = new Dictionary<string, AssetRefData>();
    private HashSet<string> _releaseAssets = new HashSet<string>();

    public AssetRefData Find(Object resource)
    {
        var ret = _addressableRefDatas.Values.ToList().Find(x => x.GetAssetOrigin == resource);
        return ret;
    }
    
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
                    Debug.LogError($"���ҽ� �ε� ���� : {path}");
                    return null;
                }

                AssetRefData refData = new AssetRefData(path, handle);
                Debug.Log($"���ҽ� �ε� ����: {path}");

                _addressableRefDatas[path] = refData;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {path}\n{e.Message}");
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
        //�̹� ������ ����̶�� ��
        if (_releaseAssets.Contains(key) == true)
            return;

        _releaseAssets.Add(key);
        int elapsed = 0;
        int interval = 100;     //0.1�ʸ��� Ȯ��
        var refData = _addressableRefDatas[key];

        while (elapsed < delayMs)
        {
            await UniTask.Delay(interval);
            elapsed += interval;

            if (refData.count > 0)
            {
                //������ ���
                Debug.Log($"������ ���: {key}, {refData.count}");
                return;
            }
        }

        if (refData.count <= 0)
        {
            //�ƹ� �������� ������� ����
            _addressableRefDatas.Remove(key);
            refData.Release();
            Debug.Log($"���� ������ {key}");
        }

        _releaseAssets.Remove(key);
    }
}