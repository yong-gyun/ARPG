using UnityEngine;

public class PositionAnimator : EffectAnimator
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Vector3 _position;
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    [SerializeField] private bool _useDuration;

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

    }
}
