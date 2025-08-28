using System.Collections.Generic;
using UnityEngine;

public partial class Effect : MonoBehaviour
{
    public Creature Owner { get; set; }
    public Creature Target { get; set; }

    public List<EffectRig> externalRigs = new List<EffectRig>();



    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        Clear();
    }

    public void Initialized()
    {

    }

    public void Clear()
    {
        for (int i = 0; i < externalRigs.Count; i++)
            Destroy(externalRigs[i]);
    }
}
