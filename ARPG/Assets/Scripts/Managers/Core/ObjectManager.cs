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

    public Hunter Hunter { get; private set; }

    public List<Creature> Monsters { get; private set; } = new List<Creature>();

    public List<BoundObject> Bounds { get; private set; } = new List<BoundObject>();

    public async UniTask<Creature> Spawn(int templateID)
    {
        Define.CreatureType creatureType = templateID.GetCreatureType();
        GameObject go = await Managers.Resource.InstantiateAsync("Creature", $"{creatureType}");
        if (go == null)
            return null;

        Creature creature = go.GetOrAddComponent<Creature>();
        creature.SetInfo(templateID);
        creature.SetStat(templateID);

        IObject obj = creature.GetComponent<IObject>();
        if (obj != null) 
            await creature.SetObject();
        
        creature.Initialized();

        switch (creature.CreatureType)
        {
            case Define.CreatureType.Hunter:
                Hunter = creature as Hunter;
                break;
            case Define.CreatureType.Monster:
                Monsters.Add(creature);
                break;
        }

        creature.Initialized();
        return creature;
    }

    public Transform CreateRoot(string name)
    {
        Transform root = new GameObject(name).transform;
        return root;
    }

    public void Clear()
    {
        Hunter = null;
        Monsters.Clear();
        Bounds.Clear();
    }
}