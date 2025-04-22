using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class CreatureInfoScriptLoader : ILoader<CreatureInfoScript>
    {
        public List<CreatureInfoScript> result { get; set; }

        public List<CreatureInfoScript> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Creature/CreatureInfo.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Creature/CreatureInfo.bytes";     
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
    public class CreatureInfoScript
    {
		public int TemplateID { get; set; }
		public string PrefabName { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
    }
}
