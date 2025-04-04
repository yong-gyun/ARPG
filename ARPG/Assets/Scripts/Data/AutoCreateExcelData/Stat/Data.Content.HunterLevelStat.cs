using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace Data.Contents
{
    public class HunterLevelStatLoader : ILoader<HunterLevelStat>
    {
        public List<HunterLevelStat> result { get; set; }

        public List<HunterLevelStat> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Stat/HunterLevelStat.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Stat/HunterLevelStat.bytes";     
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
    public class HunterLevelStat
    {
		public int HunterID { get; set; }
		public int Hp { get; set; }
		public int Attack { get; set; }
		public int Mp { get; set; }
		public int Defense { get; set; }

    }
}
