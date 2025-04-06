using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class HunterGroup : MonoBehaviour
{
    public Hunter CurrentHunter { get; set; }
    private List<Hunter> _hunters;

    public IObservable<Hunter> OnChangeHunterCallback { get { return _onChangeHunterCallback.AsObservable(); } }

    private Subject<Hunter> _onChangeHunterCallback = new Subject<Hunter>();

    public void OnChangeHunter(int index)
    {

    }

    public void RegisterHunter(Hunter hunter)
    {
        _hunters.Add(hunter);
    }
}
