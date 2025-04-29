using UniRx;
using Data.Contents;
using System.Collections.Generic;
using System.Collections.Generic.Serialized;
using UnityEngine;
using System.Linq;

public class EffectDelayActive : EffectBase
{
    [SerializeField] private float _delay;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private bool _isActive;

    private float _currentTime;
    private bool _completed;

    public override void Start()
    {
        _currentTime = 0f;
        _completed = false;
    }

    public override void Update()
    {
        if (_completed == true)
            return;

        _currentTime += Time.deltaTime;
        if (_currentTime >= _delay)
        {
            _targetObject.SetActive(_isActive);
            _completed = true;
        }
    }
}