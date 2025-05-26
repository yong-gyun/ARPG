using Common.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false, bool includeInActive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == true)
        {
            foreach (T component in go.GetComponentsInChildren<T>(includeInActive))
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                T component = child.GetComponent<T>();
                
                if (component != null)
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false, bool includeInActive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive, includeInActive);
        if (transform != null)
            return transform.gameObject;

        return null;
    }

    public static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}