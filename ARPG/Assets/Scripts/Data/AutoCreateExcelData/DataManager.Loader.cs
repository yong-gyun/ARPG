using Data.Contents;
using Data.Contents.LoaderForm;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace Data.Contents.LoaderForm
{
    public interface ILoader<T>
    {
        public List<T> result { get; set; }
        public List<T> Read();
    }

    public interface ILoader<TKey, TValue>
    {
        public List<TValue> result { get; set; }
        public Dictionary<TKey, TValue> MakeDict();
    }
}

public partial class DataManager
{
	public List<BaseStatScript> GetBaseStatScripts { get; private set; }
	public List<HunterLevelStatScript> GetHunterLevelStatScripts { get; private set; }
	public List<OverclockStatScript> GetOverclockStatScripts { get; private set; }
	public List<CreatureInfoScript> GetCreatureInfoScripts { get; private set; }
	public List<ConstValueScript> GetConstValueScripts { get; private set; }
	public List<EffectInfoScript> GetEffectInfoScripts { get; private set; }
	public List<HunterEffectInfoScript> GetHunterEffectInfoScripts { get; private set; }


    public async UniTask LoadAll()
    {
		GetBaseStatScripts = await Load<BaseStatScriptLoader, BaseStatScript>("Stat", "BaseStat");
		GetHunterLevelStatScripts = await Load<HunterLevelStatScriptLoader, HunterLevelStatScript>("Stat", "HunterLevelStat");
		GetOverclockStatScripts = await Load<OverclockStatScriptLoader, OverclockStatScript>("Stat", "OverclockStat");
		GetCreatureInfoScripts = await Load<CreatureInfoScriptLoader, CreatureInfoScript>("Creature", "CreatureInfo");
		GetConstValueScripts = await Load<ConstValueScriptLoader, ConstValueScript>("Common", "ConstValue");
		GetEffectInfoScripts = await Load<EffectInfoScriptLoader, EffectInfoScript>("Effect", "EffectInfo");
		GetHunterEffectInfoScripts = await Load<HunterEffectInfoScriptLoader, HunterEffectInfoScript>("Effect", "HunterEffectInfo");

    }

#if UNITY_EDITOR
    public void ConvertBinary()
    {
		BaseStatScriptLoader.ConvertBinary();
		HunterLevelStatScriptLoader.ConvertBinary();
		OverclockStatScriptLoader.ConvertBinary();
		CreatureInfoScriptLoader.ConvertBinary();
		ConstValueScriptLoader.ConvertBinary();
		EffectInfoScriptLoader.ConvertBinary();
		HunterEffectInfoScriptLoader.ConvertBinary();

    }
#endif

    public void Clear()
    {
		GetBaseStatScripts.Clear();
		GetHunterLevelStatScripts.Clear();
		GetOverclockStatScripts.Clear();
		GetCreatureInfoScripts.Clear();
		GetConstValueScripts.Clear();
		GetEffectInfoScripts.Clear();
		GetHunterEffectInfoScripts.Clear();

    }

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
            Managers.Resource.Release(Managers.Resource.CheckDir(dir, "Data"), key + ".json", 1, true);
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
                Managers.Resource.Release(Managers.Resource.CheckDir(dir, "Data"), key, 1, true);
                return result;
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"Failed read {key}.json");
        }

        return null;
    }
}