using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class OverclockStatScriptLoader : ILoader<OverclockStatScript>
    {
        public List<OverclockStatScript> result { get; set; }

        public List<OverclockStatScript> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Stat/OverclockStat.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Stat/OverclockStat.bytes";     
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
    public class OverclockStatScript
    {
		public Define.OverClockStatType OverClock { get; set; }
		public Define.StatType Stat { get; set; }
		public Define.EffectType Effect { get; set; }
		public float Value { get; set; }
    }
}
