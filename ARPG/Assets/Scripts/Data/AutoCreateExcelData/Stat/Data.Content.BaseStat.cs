using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace Data.Contents
{
    public class BaseStatLoader : ILoader<BaseStat>
    {
        public List<BaseStat> result { get; set; }

        public List<BaseStat> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Stat/BaseStat.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Stat/BaseStat.bytes";     
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
    public class BaseStat
    {
		public int Hp { get; set; }
		public int Energy { get; set; }
		public int Defense { get; set; }
		public int Physical { get; set; }
		public int Magical { get; set; }

    }
}
