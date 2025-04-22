using Data.Contents;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public partial class DataManager
{
	public List<BaseStatScript> GetBaseStatScripts { get; private set; }
	public List<HunterLevelStatScript> GetHunterLevelStatScripts { get; private set; }
	public List<OverclockStatScript> GetOverclockStatScripts { get; private set; }
	public List<CreatureInfoScript> GetCreatureInfoScripts { get; private set; }


    public async UniTask LoadAll()
    {
		GetBaseStatScripts = await Load<BaseStatScriptLoader, BaseStatScript>("Stat", "BaseStat");
		GetHunterLevelStatScripts = await Load<HunterLevelStatScriptLoader, HunterLevelStatScript>("Stat", "HunterLevelStat");
		GetOverclockStatScripts = await Load<OverclockStatScriptLoader, OverclockStatScript>("Stat", "OverclockStat");
		GetCreatureInfoScripts = await Load<CreatureInfoScriptLoader, CreatureInfoScript>("Creature", "CreatureInfo");

    }

#if UNITY_EDITOR
    public void ConvertBinary()
    {
		BaseStatScriptLoader.ConvertBinary();
		HunterLevelStatScriptLoader.ConvertBinary();
		OverclockStatScriptLoader.ConvertBinary();
		CreatureInfoScriptLoader.ConvertBinary();

    }
#endif

    public void Clear()
    {
		GetBaseStatScripts.Clear();
		GetHunterLevelStatScripts.Clear();
		GetOverclockStatScripts.Clear();
		GetCreatureInfoScripts.Clear();

    }
}