using System;
using UniRx;
using UnityEngine;

public class ColliderEventHandler : MonoBehaviour
{
    public IObservable<Collider> OnTriggerEnterCallback { get { return _onTriggerEnterCallback.AsObservable(); } }
    public IObservable<Collider> OnTriggerStayCallback { get { return _onTriggerStayCallback.AsObservable(); } }
    public IObservable<Collider> OnTriggerExitCallback { get { return _onTriggerExitCallback.AsObservable(); } }

    private Subject<Collider> _onTriggerEnterCallback = new Subject<Collider>();
    private Subject<Collider> _onTriggerStayCallback = new Subject<Collider>();
    private Subject<Collider> _onTriggerExitCallback = new Subject<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnterCallback.OnNext(other);        
    }

    private void OnTriggerStay(Collider other)
    {
        _onTriggerStayCallback.OnNext(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _onTriggerExitCallback.OnNext(other);
    }

    public void Clear()
    {
        _onTriggerEnterCallback.Dispose();
        _onTriggerStayCallback.Dispose();
        _onTriggerExitCallback.Dispose();
    }
}
