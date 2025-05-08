using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveEffect : BaseEffect
{
    [SerializeField] private int activeDelay;
    [SerializeField] private int deactiveDelay;

    [SerializeField] private List<GameObject> _activeObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _deactiveObjects = new List<GameObject>();

    public override void PlayAction()
    {
        ActiveObjects();
        DeactiveObjects();
    }

    private async void ActiveObjects()
    {
        await UniTask.Delay(activeDelay);

        foreach (var obj in _activeObjects)
            obj.SetActive(true);
    }

    private async void DeactiveObjects()
    {
        await UniTask.Delay(deactiveDelay);

        foreach (var obj in _deactiveObjects)
            obj.SetActive(false);
    }
}
