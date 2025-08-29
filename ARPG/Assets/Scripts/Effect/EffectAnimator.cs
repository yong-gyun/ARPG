using UnityEngine;

public class EffectAnimator : MonoBehaviour
{
    public float Start { get { return _start; } }
    public float Elapsed { get { return _elapsed; } }
    public float LocalElapsed { get { return Mathf.Max(_elapsed - _start, 0f); } }

    [SerializeField] protected float _start = 0f;
    [SerializeField] protected float _elapsed = 0f;

    public virtual void OnUpdate(float deltaTime)
    {
        _elapsed += deltaTime;
    }
}