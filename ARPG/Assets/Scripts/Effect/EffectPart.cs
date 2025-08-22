using DG.Tweening;
using System;
using UnityEngine;

public abstract class ObjectAction : BaseEffectEvent
{
    protected Tween _actionTween;

    [SerializeField] protected GameObject _targetObject;

    [SerializeField] protected float _duration;
    
    [SerializeField] protected float _speed;

    [SerializeField] protected bool _useSpeed;

    [SerializeField] protected Ease _easeType;

    [SerializeField] protected LoopType _loopType;

    [SerializeField] protected int _loop;

    public abstract override void OnAction();
}

[Serializable]
public class MoveObject : ObjectAction
{
    [SerializeField] private Vector3 _offset;

    public override void OnAction()
    {
        var dest = _targetObject.transform.position + _offset;
        _actionTween = _targetObject.transform.DOMove(dest, _useSpeed == true ? _speed : _duration).
            SetAutoKill().
            SetEase(_easeType).
            SetSpeedBased(_useSpeed).
            SetLoops(_loop, _loopType);

        _actionTween.Play();
    }
}

[Serializable]
public class RotationObject : ObjectAction
{
    [SerializeField] private RotateMode _rotateMode;

    [SerializeField] private Vector3 _rotation;

    public override void OnAction()
    {
        _actionTween = _targetObject.transform.DORotate(_rotation, _useSpeed == true ? _speed : _duration, _rotateMode).
            SetAutoKill().
            SetEase(_easeType).
            SetSpeedBased(_useSpeed).
            SetLoops(_loop, _loopType);

        _actionTween.Play();
    }
}

[Serializable]
public class ScaleObject : ObjectAction
{
    [SerializeField] private Vector3 _scale;

    public override void OnAction()
    {
        _actionTween = _targetObject.transform.DOScale(_scale, _useSpeed == true ? _speed : _duration).
            SetAutoKill().
            SetEase(_easeType).
            SetSpeedBased(_useSpeed).
            SetLoops(_loop, _loopType);

        _actionTween.Play();
    }
}

public class EffectPart : MonoBehaviour
{
    [SerializeField] private bool _isMoveActive;
    [SerializeField] private bool _isRotationActive;
    [SerializeField] private bool _isScaleActive;

    [SerializeField] private MoveObject _moveEvent = new MoveObject();
    [SerializeField] private RotationObject _rotationEvent = new RotationObject();
    [SerializeField] private ScaleObject _scaleEvent = new ScaleObject();

    private void OnEnable() => PlayActios();

    public void PlayActios()
    {
        if (_isMoveActive == true)
            _moveEvent.Action();

        if (_isRotationActive == true)
            _rotationEvent.Action();

        if (_isScaleActive == true)
            _scaleEvent.Action();
    }
}