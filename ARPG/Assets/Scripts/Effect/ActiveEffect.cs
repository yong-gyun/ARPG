using System.Collections.Generic;
using UnityEngine;

public class ActiveEffect : BaseEffect
{
    [SerializeField] private List<GameObject> _activeObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _deactiveObjects = new List<GameObject>();

    public override void OnAction()
    {
        foreach (var obj in _activeObjects) 
            obj.SetActive(true);

        foreach (var obj in _deactiveObjects)
            obj.SetActive(false);
    }
}
