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
	public List<ConstValueScript> GetConstValueScripts { get; private set; }
	public List<BaseStatScript> GetBaseStatScripts { get; private set; }
	public List<HunterLevelStatScript> GetHunterLevelStatScripts { get; private set; }
	public List<OverclockStatScript> GetOverclockStatScripts { get; private set; }
	public List<CreatureInfoScript> GetCreatureInfoScripts { get; private set; }
	public List<SkillLeveInfoScript> GetSkillLeveInfoScripts { get; private set; }
	public List<SkillInfoScript> GetSkillInfoScripts { get; private set; }


    public async UniTask LoadAll()
    {
		GetConstValueScripts = await Load<ConstValueScriptLoader, ConstValueScript>("Common", "ConstValue");
		GetBaseStatScripts = await Load<BaseStatScriptLoader, BaseStatScript>("Stat", "BaseStat");
		GetHunterLevelStatScripts = await Load<HunterLevelStatScriptLoader, HunterLevelStatScript>("Stat", "HunterLevelStat");
		GetOverclockStatScripts = await Load<OverclockStatScriptLoader, OverclockStatScript>("Stat", "OverclockStat");
		GetCreatureInfoScripts = await Load<CreatureInfoScriptLoader, CreatureInfoScript>("Creature", "CreatureInfo");
		GetSkillLeveInfoScripts = await Load<SkillLeveInfoScriptLoader, SkillLeveInfoScript>("Creature", "SkillLeveInfo");
		GetSkillInfoScripts = await Load<SkillInfoScriptLoader, SkillInfoScript>("Creature", "SkillInfo");

    }

#if UNITY_EDITOR
    public void ConvertBinary()
    {
		ConstValueScriptLoader.ConvertBinary();
		BaseStatScriptLoader.ConvertBinary();
		HunterLevelStatScriptLoader.ConvertBinary();
		OverclockStatScriptLoader.ConvertBinary();
		CreatureInfoScriptLoader.ConvertBinary();
		SkillLeveInfoScriptLoader.ConvertBinary();
		SkillInfoScriptLoader.ConvertBinary();

    }
#endif

    public void Clear()
    {
		GetConstValueScripts.Clear();
		GetBaseStatScripts.Clear();
		GetHunterLevelStatScripts.Clear();
		GetOverclockStatScripts.Clear();
		GetCreatureInfoScripts.Clear();
		GetSkillLeveInfoScripts.Clear();
		GetSkillInfoScripts.Clear();

    }
}