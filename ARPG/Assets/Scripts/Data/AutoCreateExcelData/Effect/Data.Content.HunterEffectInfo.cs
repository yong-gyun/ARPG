using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Data.Contents.LoaderForm;

namespace Data.Contents
{
    public class HunterEffectInfoScriptLoader : ILoader<HunterEffectInfoScript>
    {
        public List<HunterEffectInfoScript> result { get; set; }

        public List<HunterEffectInfoScript> Read()
        {
            return result;
        }
#if UNITY_EDITOR
        public static void ConvertBinary()
        {
            string path = "Assets/AssetBundles/Data/Effect/HunterEffectInfo.json";
            var load = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            JsonSerializerSettings settings = new JsonSerializerSettings();
       
            var convertPath = "Assets/AssetBundles/Data/Effect/HunterEffectInfo.bytes";     
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
    public class HunterEffectInfoScript
    {
		public int CreatureID { get; set; }
		public int PassiveID { get; set; }
		public int NormalAttack1ID { get; set; }
		public int NormalAttack2ID { get; set; }
		public int NormalAttack3ID { get; set; }
		public int NormalAttack4ID { get; set; }
		public int UltSkillID { get; set; }
    }
}
