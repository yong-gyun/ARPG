using Cysharp.Text;
using Cysharp.Threading.Tasks;
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
        return LoadAsync<GameObject>(dir, CheckKey(key, ".prefab"));
    }

    public UniTask<Sprite> LoadSpriteAsync(string dir, string key)
    {
        return LoadAsync<Sprite>(dir, CheckKey(key, ".sprite"));
    }

    private string CheckKey(string key, string checkFormmat)
    {
        string result = key;
        if (key.Contains(checkFormmat) == false)
            result = ZString.Concat(key, checkFormmat);

        return result;
    }
}
