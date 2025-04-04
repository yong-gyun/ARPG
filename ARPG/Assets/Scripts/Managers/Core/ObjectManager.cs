using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public Transform HunterRoot
    {
        get
        {
            if (_hunterRoot == null)
                _hunterRoot = CreateRoot("@Hunter_Root");

            return _hunterRoot;
        }
    }

    private Transform _hunterRoot;

    public HashSet<Hunter> Hunters { get; private set; } = new HashSet<Hunter>();

    public async UniTask<Hunter> SpawnHunter(int hunterID)
    {
        GameObject go = await Managers.Resource.InstantiateAsync("Creature", "Hunter");
        if (go == null)
            return null;

        Hunter hunter = go.GetOrAddComponent<Hunter>();
        hunter.SetInfo(hunterID);
        return hunter;
    }

    public Transform CreateRoot(string name)
    {
        Transform root = new GameObject(name).transform;
        return root;
    }
}