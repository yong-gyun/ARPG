using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBase : MonoBehaviour
{
    private bool _init;

    private void Awake()
    {
        Init();
    }

    public virtual bool Init()
    {
        if (true == _init)
            return false;

        _init = true;
        return true;
    }

    public virtual void Clear()
    {

    }
}
