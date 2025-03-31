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
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


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
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

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

    public override void Clear()
    {{
{3}
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
