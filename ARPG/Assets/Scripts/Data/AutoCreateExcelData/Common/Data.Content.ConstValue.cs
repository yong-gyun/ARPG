using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class ConstValueScriptLoader : ILoader<ConstValueScript>
    {
        public List<ConstValueScript> result { get; set; }

        public List<ConstValueScript> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Common/ConstValue.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Common/ConstValue.bytes";     
            File.WriteAllBytes(convertPath, load.bytes);
            try
            {
                File.Delete(path);
            }
            catch
            {

            }
        }
#endif
    }

    [Serializable]
    public class ConstValueScript
    {
		public Define.ConstDefType ConstType { get; set; }
		public float Value { get; set; }

    }
}
