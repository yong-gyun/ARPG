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

    public async UniTask<Creature> Spawn(int templateID)
    {
        Define.CreatureType creatureType = templateID.GetCreatureType();
        GameObject go = await Managers.Resource.InstantiateAsync("Creature", $"{creatureType}");
        if (go == null)
            return null;

        Creature creature = go.GetOrAddComponent<Creature>();
        await creature.Init(templateID);
        return creature;
    }

    public Transform CreateRoot(string name)
    {
        Transform root = new GameObject(name).transform;
        return root;
    }
}