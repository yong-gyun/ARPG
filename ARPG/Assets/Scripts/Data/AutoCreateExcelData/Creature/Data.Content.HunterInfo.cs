using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace Data.Contents
{
    public class HunterInfoLoader : ILoader<HunterInfo>
    {
        public List<HunterInfo> result { get; set; }

        public List<HunterInfo> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Creature/HunterInfo.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Creature/HunterInfo.bytes";     
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
    public class HunterInfo
    {
		public int HunterID { get; set; }
		public Define.ClassType Class { get; set; }
		public string PrefabName { get; set; }
		public string HunterName { get; set; }
    }
}
