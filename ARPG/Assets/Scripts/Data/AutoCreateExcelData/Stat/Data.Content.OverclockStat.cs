using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace Data.Contents
{
    public class OverclockStatLoader : ILoader<OverclockStat>
    {
        public List<OverclockStat> result { get; set; }

        public List<OverclockStat> Read()
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
    public class OverclockStat
    {
		public Define.OverClockStatType OverClock { get; set; }
		public Define.StatType Stat { get; set; }
		public Define.EffectType Effect { get; set; }
		public int Value { get; set; }
    }
}
