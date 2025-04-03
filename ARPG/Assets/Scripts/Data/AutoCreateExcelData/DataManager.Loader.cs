using Data.Contents;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public partial class DataManager
{
	public List<BaseStat> GetBaseStatDatas { get; private set; }


    public async UniTask LoadAll()
    {
		GetBaseStatDatas = await Load<BaseStatLoader, BaseStat>("Stat", "BaseStat");

    }

#if UNITY_EDITOR
    public void ConvertBinary()
    {
		BaseStatLoader.ConvertBinary();

    }
#endif

    public void Clear()
    {
		GetBaseStatDatas.Clear();

    }
}