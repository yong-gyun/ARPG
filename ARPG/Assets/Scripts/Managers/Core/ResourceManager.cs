using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public void Release(string dir, string asset, int releaseCount = 1, bool releaseImmediate = false)
    {
        assetManager.Release(ZString.Concat(dir, "/", asset), releaseCount, releaseImmediate);
    }
}
