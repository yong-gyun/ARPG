using Data.Contents;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public partial class DataManager
{
	public List<BaseStat> GetBaseStatDatas { get; private set; }
	public List<HunterLevelStat> GetHunterLevelStatDatas { get; private set; }
	public List<OverclockStat> GetOverclockStatDatas { get; private set; }
	public List<HunterInfo> GetHunterInfoDatas { get; private set; }


    public async UniTask LoadAll()
    {
		GetBaseStatDatas = await Load<BaseStatLoader, BaseStat>("Stat", "BaseStat");
		GetHunterLevelStatDatas = await Load<HunterLevelStatLoader, HunterLevelStat>("Stat", "HunterLevelStat");
		GetOverclockStatDatas = await Load<OverclockStatLoader, OverclockStat>("Stat", "OverclockStat");
		GetHunterInfoDatas = await Load<HunterInfoLoader, HunterInfo>("Creature", "HunterInfo");

    }

#if UNITY_EDITOR
    public void ConvertBinary()
    {
		BaseStatLoader.ConvertBinary();
		HunterLevelStatLoader.ConvertBinary();
		OverclockStatLoader.ConvertBinary();
		HunterInfoLoader.ConvertBinary();

    }
#endif

    public void Clear()
    {
		GetBaseStatDatas.Clear();
		GetHunterLevelStatDatas.Clear();
		GetOverclockStatDatas.Clear();
		GetHunterInfoDatas.Clear();

    }
}