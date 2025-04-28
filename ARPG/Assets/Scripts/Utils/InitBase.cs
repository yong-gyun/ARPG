using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBase : MonoBehaviour
{
    protected bool _init;

    public virtual bool Init(int templateID)
    {
        if (true == _init)
            return false;

        _init = true;
        return true;
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
