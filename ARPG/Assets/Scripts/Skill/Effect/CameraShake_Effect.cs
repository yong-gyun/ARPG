using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class CameraShakeEffect : BaseEffect
{
    [SerializeField] private float _strength;
    [SerializeField] private float _duration;

    public override void PlayAction()
    {
        Camera camera = Camera.main;
        camera.DOShakePosition(_duration, _strength);
    }
}