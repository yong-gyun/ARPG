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

    private bool _activeEffect;

    private int _destroyTime;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
            Debug.Log(child.name);
        }
    }

    public void Init(GameObject owner)
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
            {
                try
                {
                    mit.PlayAction();
                }
                catch(Exception e)
                {
                    Debug.LogError($"Effect Action Error {mit.GetType().Name}");   
                }
            }
        }
        catch (Exception e)
        {

        }
    }
}
