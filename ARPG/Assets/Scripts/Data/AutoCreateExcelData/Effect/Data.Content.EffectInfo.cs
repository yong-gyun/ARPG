using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class EffectInfoScriptLoader : ILoader<EffectInfoScript>
    {
        public List<EffectInfoScript> result { get; set; }

        public List<EffectInfoScript> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Effect/EffectInfo.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Effect/EffectInfo.bytes";     
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
    public class EffectInfoScript
    {
        public int SkillID { get; set; }
		public int SkillArg { get; set; }
		public Define.EffectType Effect { get; set; }
		public int Lv1 { get; set; }
		public int Lv2 { get; set; }
		public int Lv3 { get; set; }
		public int Lv4 { get; set; }
		public int Lv5 { get; set; }
		public int Lv6 { get; set; }
		public int Lv7 { get; set; }
		public int Lv8 { get; set; }
		public int Lv9 { get; set; }
		public int Lv10 { get; set; }
    }
}
