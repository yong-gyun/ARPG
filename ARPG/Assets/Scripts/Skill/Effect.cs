using Data.Contents;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private List<EffectBase> _effects = new List<EffectBase>();
    public int SkillID { get; private set; }

    private bool _init;
    
    public void Init(int templateID)
    {
        SkillID = templateID;
        foreach (var effect in _effects)
            effect.Init(this);

        _init = true;
    }

    private void Start()
    {
        foreach (var item in _effects)
            item.Start();
    }

    private void Update()
    {
        foreach (var item in _effects)
            item.Update();
    }

    private void OnDestroy()
    {
        foreach (var item in _effects)
            item.Destory();
    }
}
