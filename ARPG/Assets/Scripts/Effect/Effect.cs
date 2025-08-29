using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public partial class Effect : BaseObject
{
    public Creature Owner { get; set; }
    public Creature Target { get; set; }

    public bool IsEnd { get { return _elapsed >= _length; } }

    public List<EffectRig> externalRigs = new List<EffectRig>();

    private List<EffectRig> _rigs = new List<EffectRig>();

    private List<EffectAnimator> _animators = new List<EffectAnimator>();

    private EventActionBehaviour _behaviour;

    [SerializeField] private float _length;
    
    public override bool Initialized()
    {
        if (base.Initialized() == false)
            return false;

        SetRunner(Owner.Runner);
        return true;
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        foreach (EffectRig rig in _rigs)
            rig.OnUpdate(deltaTime);

        foreach(EffectAnimator animator in _animators) 
            animator.OnUpdate(deltaTime);

        _behaviour.OnUpdate(Elapsed, _runner);
    }

    public void Collect()
    {
        Clear();

        _rigs = transform.GetComponentsInChildren<EffectRig>(true).ToList();
        _animators = transform.GetComponentsInChildren<EffectAnimator>(true).ToList();
    }

    public void Clear()
    {
        for (int i = 0; i < externalRigs.Count; i++)
            Destroy(externalRigs[i]);

        externalRigs.Clear();
        _rigs.Clear();
        _animators.Clear();
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);
    }

    private void OnEnable()
    {
        Initialized();    
    }

    private void OnDestroy()
    {
        Clear();
    }

    private void Reset()
    {
        Collect();
    }
}