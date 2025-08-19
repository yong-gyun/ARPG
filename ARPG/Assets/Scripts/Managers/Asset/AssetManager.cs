using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

public class AssetManager
{
    public const string DOWNLOAD_LABEL = "download";
    

    private AssetPool _assetPool = new AssetPool();

    public UniTask<T> LoadAsync<T>(string dir, string asset) where T : Object
    {
        return _assetPool.LoadAsync<T>(ZString.Concat(Config.ASSET_PATH, "/", dir, "/", asset));
    }

    public void Release(string key, int releaseCount, bool releaseImmediate)
    {
        _assetPool.Release(ZString.Concat(Config.ASSET_PATH, "/", key), releaseCount, releaseImmediate);
    }
}