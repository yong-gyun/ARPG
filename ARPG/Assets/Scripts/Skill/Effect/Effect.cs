using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.Generic.Serialized;
using System.Linq;
using UnityEngine;

public class Effect : MonoBehaviour
{   
    [SerializeReference, SubclassSelector]
    private List<BaseEffect> _effects = new List<BaseEffect>();         //이펙트

    [SerializeField] private int _activeTime = 0;      //n ms 후 오브젝트 활성화

    [SerializeField] private int _deactiveTime = 1000;    //n ms 후에 오브젝트 비활성화

    public void Init(Creature owner)
    {
        foreach (var mit in _effects)
            mit.Init(owner);
    }

    public async void PlayAction(int command)
    {
        await UniTask.Delay(_activeTime);

        //Time.timeScale = 0.2f;
        var activeEffects = _effects.FindAll(x => x.command == command).ToList();
        foreach (var mit in activeEffects)
            mit.PlayAction();

        await UniTask.Delay(_deactiveTime);
        //Time.timeScale = 1f;

        Managers.Resource.Destroy(gameObject);
    }
}
