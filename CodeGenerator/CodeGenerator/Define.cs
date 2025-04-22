using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Define
{
    public const string JSON_FORMAT =
@"
    {{
{0}
    }},";

    public const string CLIENT_DATA_CONTENT_FORMAT =
@"using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{{
    public class {0}Loader : ILoader<{0}>
    {{
        public List<{0}> result {{ get; set; }}

        public List<{0}> Read()
        {{
            return result;
        }}
#if UNITY_EDITOR
        public static void ConvertBinary()
        {{
            string path = ""{2}.json"";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = ""{2}.bytes"";     
            File.WriteAllBytes(convertPath, load.bytes);
            try
            {{
                File.Delete(path);
            }}
            catch
            {{

            }}
        }}
#endif
    }}

    [Serializable]
    public class {0}
    {{
{1}
    }}
}}
";

    public const string DATA_MANAGER_FORMAT =
@"using Data.Contents;
using Data.Contents.LoaderForm;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace Data.Contents.LoaderForm
{{
    public interface ILoader<T>
    {{
        public List<T> result {{ get; set; }}
        public List<T> Read();
    }}

    public interface ILoader<TKey, TValue>
    {{
        public List<TValue> result {{ get; set; }}
        public Dictionary<TKey, TValue> MakeDict();
    }}
}}

public partial class DataManager
{{
{0}

    public async UniTask LoadAll()
    {{
{1}
    }}

#if UNITY_EDITOR
    public void ConvertBinary()
    {{
{2}
    }}
#endif

    public void Clear()
    {{
{3}
    }}

    public async UniTask<List<TItem>> Load<TLoader, TItem>(string dir, string key) where TLoader : ILoader<TItem>
    {{
        List<TItem> result = null;
#if UNITY_EDITOR == false || TEST_DOWNLOAD == true
        try
        {{
            TextAsset textAsset = await Managers.Resource.LoadByte(key);
            using (MemoryStream stream = new MemoryStream(textAsset.bytes))
            {{
                
            }}
        }}
        catch (Exception e)
        {{
            Debug.LogError($""Failed read {{key}}.byte"");
        }}

        result = null;
#endif
        try
        {{
            if (result == null)
            {{
                TextAsset textAsset = await Managers.Resource.LoadJson(dir, key);
                result = JsonConvert.DeserializeObject<TLoader>(""{{ \""result\"" : "" + textAsset.text + ""}}"").Read();
                Debug.Log($""Load {{key}}.json"");
            }}
        }}
        catch (Exception e)
        {{
            Debug.LogError($""Failed read {{key}}.json\n {{e}}"");
        }}
        finally
        {{
            Managers.Resource.Release(Managers.Resource.CheckDir(dir, ""Data""), key + "".json"", 1, true);
        }}

        return result;
    }}

    public async UniTask<Dictionary<TKey, TValue>> Load<TLoader, TKey, TValue>(string dir, string key) where TLoader : ILoader<TKey, TValue>
    {{
        try
        {{
            TextAsset textAsset = await Managers.Resource.LoadJson(dir, key);
            Dictionary<TKey, TValue> result = JsonConvert.DeserializeObject<TLoader>(""{{ \""result\"" : "" + textAsset.text + ""}}"").MakeDict();

            if (result != null)
            {{
                Managers.Resource.Release(Managers.Resource.CheckDir(dir, ""Data""), key, 1, true);
                return result;
            }}

        }}
        catch (Exception e)
        {{
            Debug.LogError($""Failed read {{key}}.json"");
        }}

        return null;
    }}
}}";

    public const string DATA_MANAGER_FIELD_FORMAT = "\t\tpublic {0} {1} {{ get; set; }}";
    public const string DATA_LOAD_FORMAT = "\t\t{0} = await Load<{1}Loader, {1}>(\"{2}\");";

    public enum INI_KEY
    {
        Excel,
        ClientJson,
        ClientSource,
        ClientPacket,
        ServerJson,
        ServerSource,
        ServerPacket,
        CheckBuildClientJson,
        CheckBuildServerJson,
    }
}
