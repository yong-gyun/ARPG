using Cysharp.Threading.Tasks;
using Data.Contents;
using Data.Contents.LoaderForm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class DataManager
{
    public Dictionary<int, List<SkillInfoScript>> SkillInfoDict { get; private set; } = new Dictionary<int, List<SkillInfoScript>>();
    public bool Loaded { get; set; }

    public async UniTask<List<TItem>> Load<TLoader, TItem>(string dir, string key) where TLoader : ILoader<TItem>
    {
        List<TItem> result = null;
#if UNITY_EDITOR == false || TEST_DOWNLOAD == true
        try
        {
            TextAsset textAsset = await Managers.Resource.LoadByte(key);
            using (MemoryStream stream = new MemoryStream(textAsset.bytes))
            {
                
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed read {key}.byte");
        }

        result = null;
#endif
        try
        {
            if (result == null)
            {
                TextAsset textAsset = await Managers.Resource.LoadJson(dir, key);
                result = JsonConvert.DeserializeObject<TLoader>("{ \"result\" : " + textAsset.text + "}").Read();
                Debug.Log($"Load {key}.json");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed read {key}.json\n {e}");
        }
        finally
        {
            Managers.Resource.Release(Config.DATA_PATH, key + ".json", 1, true);
        }

        return result;
    }

    public async UniTask<Dictionary<TKey, TValue>> Load<TLoader, TKey, TValue>(string dir, string key) where TLoader : ILoader<TKey, TValue>
    {
        try
        {
            TextAsset textAsset = await Managers.Resource.LoadJson(dir, key);
            Dictionary<TKey, TValue> result = JsonConvert.DeserializeObject<TLoader>("{ \"result\" : " + textAsset.text + "}").MakeDict();

            if (result != null)
            {
                Managers.Resource.Release(Config.DATA_PATH, key, 1, true);
                return result;
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"Failed read {key}.json");
        }

        return null;
    }

    public void Init()
    {
        foreach (var item in GetSkillInfoScripts)
        {
            if (SkillInfoDict.ContainsKey(item.SkillID) == false)
                SkillInfoDict[item.SkillID] = new List<SkillInfoScript>();

            SkillInfoDict[item.SkillID].Add(item);
        }
    }

    public List<SkillInfoScript> GetSkillArgs(int skillID)
    {
        if (SkillInfoDict.TryGetValue(skillID, out var ret) == true)
            return ret;

        return null;
    }

    public int GetConstValue(Define.ConstDefType constDefType)
    {
        var script = GetConstValueScripts.Find(x => x.ConstType == constDefType);
        if (script == null)
            return 0;

        return script.Value;
    }
}