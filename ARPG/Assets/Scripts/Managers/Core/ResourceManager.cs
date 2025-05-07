using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.Image;
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
        return LoadAsync<GameObject>(CheckDir(dir, "Prefabs"), CheckKey(key, ".prefab"));
    }

    public UniTask<Sprite> LoadSpriteAsync(string dir, string key)
    {
        return LoadAsync<Sprite>(CheckDir(dir, "Sprites"), CheckKey(key, ".sprite"));
    }

    public UniTask<TextAsset> LoadJson(string dir, string key)
    {
        return LoadAsync<TextAsset>(CheckDir(dir, "Data"), CheckKey(key, ".json"));
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
        GameObject prefab = await LoadGameObjectAsync(dir, key);
        if (prefab == null)
            return null;
        
        if (pool == true)
        {
            string path = ZString.Concat(CheckDir(dir, "Prefabs"), "/", CheckKey(key, ".prefab"));
            GameObject go = Managers.Pool.Pop(path, prefab);
            return go;
        }
        else
        {
            return Instantiate(prefab, parent, false);
        }
    }

    public GameObject Instantiate(GameObject origin, Transform parent = null, bool pool = false)
    {
        if (origin == null)
            return null;

        GameObject go = null;
        if (pool == true)
        {
            string key = origin.name;
            go = Managers.Pool.Pop(key, origin, false);
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

    public string CheckKey(string key, string checkFormmat)
    {
        string result = key;
        if (key.Contains(checkFormmat) == false)
            result = ZString.Concat(key, checkFormmat);

        return result;
    }

    public string CheckDir(string dir, string root)
    {
        string result = dir;
        if (dir.Contains(root) == false)
            result = ZString.Concat(root, "/", dir);

        return result;
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
