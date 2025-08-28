using Common.Skill;
using Data.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class ExtendedHelper
{
    public static Define.CreatureType GetCreatureType(this int templateID)
    {
        return (Define.CreatureType) (templateID / 1000);
    }

    public static int GetCreatureID(this int templateID)
    {
        return templateID % 1000;
    }

    //백만분으로 연산 0~1
    public static float PPMToFloat(this long src)
    {
        return src * 0.000001f;
    }

    //백만분으로 연산 0~1
    public static float PPMToFloat(this int src)
    {
        return src * 0.000001f;
    }

    public static Vector3 GetLocalDir(this GameObject go, Define.DirType dir)
    {
        Vector3 ret = Vector3.zero;
        switch (dir)
        {
            case Define.DirType.Front:
                ret = go.transform.forward;
                break;
            case Define.DirType.Back:
                ret = -go.transform.forward;
                break;
            case Define.DirType.Right:
                ret = go.transform.right;
                break;
            case Define.DirType.Left:
                ret = -go.transform.right;
                break;
        }

        return ret.normalized;
    }
}