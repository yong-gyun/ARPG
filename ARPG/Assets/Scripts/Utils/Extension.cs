using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension 
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false, bool includeInActive = false) where T : UnityEngine.Object
    {
        return Util.FindChild<T>(go, name, recursive, includeInActive);
    }

    public static GameObject FindChild(this GameObject go, string name, bool recursive = false, bool includeInActive = false)
    {
        return Util.FindChild(go, name, recursive, includeInActive);
    }
}
