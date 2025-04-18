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

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        CurrentHunter.Dir = new Vector3(horizontal, 0f, vertical);
    }

    public void OnChangeHunter(int index)
    {

    }

    public void RegisterHunter(Hunter hunter)
    {
        _hunters.Add(hunter);
    }
}
