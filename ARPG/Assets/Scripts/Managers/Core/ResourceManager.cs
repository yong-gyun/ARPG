using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceManager
{
    private AssetManager assetManager = new AssetManager();

    public async UniTask<T> LoadAsync<T>(string dir, string key) where T : Object
    {
        return await assetManager.LoadAsync<T>(dir, key);
    }

    public UniTask<GameObject> LoadGameObjectAsync(string dir, string key)
    {
        if (key.Contains(".prefab") == false)
            key = ZString.Concat(key, ".prefab");

        return LoadAsync<GameObject>(Config.PREFAB_PATH, key);
    }

    public UniTask<Sprite> LoadSpriteAsync(string dir, string key)
    {
        if (key.Contains(".sprite") == false)
            key = ZString.Concat(key, ".sprite");

        return LoadAsync<Sprite>(Config.SPRITE_PATH, key);
    }

    public UniTask<TextAsset> LoadJson(string dir, string key)
    {
        if (key.Contains(".json") == false)
            key = ZString.Concat(key, ".json");

        return LoadAsync<TextAsset>(Config.DATA_PATH, key);
    }

    public async UniTask<GameObject> InstantiateAsync(string dir, string key, Vector3 pos, Quaternion rot, Transform parent = null, bool pool = false)
    {
        GameObject go = await InstantiateAsync(dir, key, parent, pool);
        if (go == null)
            return null;

        go.transform.position = pos;
        go.transform.rotation = rot;
        return go;
    }

    public async UniTask<GameObject> InstantiateAsync(string dir, string key, Transform parent = null, bool pool = false)
    {
        if (key.Contains(".prefab") == false)
            key = ZString.Concat(key, ".prefab");

        GameObject prefab = await LoadGameObjectAsync(dir, key);
        if (prefab == null)
            return null;

        GameObject go = null;
        if (pool == true)
        {
            string path = ZString.Concat(Config.PREFAB_PATH, "/", key);
            go = Managers.Pool.Pop(prefab, path);
        }
        else
        {
            go = Instantiate(prefab, parent, false);
        }

        if (go != null)
        {
            go.OnDestroyAsObservable().Subscribe(_ =>
            {
                Release(Config.PREFAB_PATH, key);
            });
        }

        return go;
    }

    public GameObject Instantiate(GameObject origin, Transform parent = null, bool pool = false)
    {
        if (origin == null)
            return null;

        GameObject go = null;
        if (pool == true)
        {
            go = Managers.Pool.Pop(origin, checkRef: false);
        }
        else
        {
            go = Object.Instantiate(origin, parent);
        }

        return go;
    }

    public GameObject Instantiate(GameObject origin, Vector3 pos, Quaternion rot, Transform parent = null, bool pool = false)
    {
        GameObject go = Instantiate(origin, parent, pool);
        if (go == null)
            return null;

        go.transform.position = pos;
        go.transform.rotation = rot;
        return go;
    }

    public async void Destroy(GameObject go, float t = 0f)
    {
        if (Managers.Pool.CheckPoolObject(go))   //풀링 오브젝트인 경우 다시 반환
        {
            await UniTask.Delay(TimeSpan.FromSeconds(t));
            Managers.Pool.Pop(go);
        }
        else
        {
            Object.Destroy(go, t);
        }
    }

    public void Release(string key, int releaseCount = 1, bool releaseImmediate = false)
    {
        assetManager.Release(key, releaseCount, releaseImmediate);
    }

    public void Release(string dir, string asset, int releaseCount = 1, bool releaseImmediate = false)
    {
        assetManager.Release(ZString.Concat(dir, "/", asset), releaseCount, releaseImmediate);
    }
}
