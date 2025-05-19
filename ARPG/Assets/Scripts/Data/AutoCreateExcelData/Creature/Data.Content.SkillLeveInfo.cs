using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class SkillLeveInfoScriptLoader : ILoader<SkillLeveInfoScript>
    {
        public List<SkillLeveInfoScript> result { get; set; }

        public List<SkillLeveInfoScript> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Creature/SkillLeveInfo.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Creature/SkillLeveInfo.bytes";     
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
    public class SkillLeveInfoScript
    {
		public int SkillID { get; set; }
		public int SkillArg { get; set; }
		public Define.EffectType Effect { get; set; }
		public long Lv1 { get; set; }
		public long Lv2 { get; set; }
		public long Lv3 { get; set; }
		public long Lv4 { get; set; }
		public long Lv5 { get; set; }
		public long Lv6 { get; set; }
		public long Lv7 { get; set; }
		public long Lv8 { get; set; }
		public long Lv9 { get; set; }
		public long Lv10 { get; set; }
    }
}
