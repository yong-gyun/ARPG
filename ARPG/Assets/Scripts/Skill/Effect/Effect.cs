using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.Generic.Serialized;
using System.Linq;
using UnityEngine;

public class Effect : MonoBehaviour
{   
    [SerializeReference, SubclassSelector]
    private List<BaseEffect> _effects = new List<BaseEffect>();         //����Ʈ

    [SerializeField] private int _activeTime = 0;      //n ms �� ������Ʈ Ȱ��ȭ

    [SerializeField] private int _deactiveTime = 1000;    //n ms �Ŀ� ������Ʈ ��Ȱ��ȭ

    private bool _activeEffect;

    private int _destroyTime;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }

    public void Init(Creature owner)
    {
        foreach (var mit in _effects)
            mit.Init(owner);
    }

    private async void Start()
    {
        await UniTask.Delay(_deactiveTime);
        Managers.Resource.Destroy(gameObject);
    }

    public async void PlayAction(int command)
    {
        try
        {
            await UniTask.Delay(_activeTime);

            var activeEffects = _effects.FindAll(x => x.command == command).ToList();
            foreach (var mit in activeEffects)
                mit.PlayAction();
        }
        catch (Exception e)
        {

        }
    }
}
