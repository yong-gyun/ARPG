using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class HunterLevelStatScriptLoader : ILoader<HunterLevelStatScript>
    {
        public List<HunterLevelStatScript> result { get; set; }

        public List<HunterLevelStatScript> Read()
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
    public class HunterLevelStatScript
    {
		public int HunterID { get; set; }
		public float Hp { get; set; }
		public float Attack { get; set; }
		public float Mp { get; set; }
		public float Defense { get; set; }

    }
}
