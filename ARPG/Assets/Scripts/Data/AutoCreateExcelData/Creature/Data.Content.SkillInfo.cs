using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class SkillInfoScriptLoader : ILoader<SkillInfoScript>
    {
        public List<SkillInfoScript> result { get; set; }

        public List<SkillInfoScript> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Creature/SkillInfo.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Creature/SkillInfo.bytes";     
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
    public class SkillInfoScript
    {
		public int SkillID { get; set; }
		public string SkillName { get; set; }
		public string SkillDesc { get; set; }
    }
}
