using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

public class AssetManager
{
    public const string DOWNLOAD_LABEL = "download";
    public const string DIRECTORY_PATH = "Assets/AddressbleAssets";

    private AssetPool _assetPool = new AssetPool();

    public UniTask<T> LoadAsync<T>(string dir, string asset) where T : Object
    {
        return _assetPool.LoadAsync<T>(GetAssetPathFormatter(dir, asset));
    }

    public string GetAssetPathFormatter(string dir, string asset)
    {
        return ZString.Concat(DIRECTORY_PATH + "/" + dir + "/" + asset);
    }
}