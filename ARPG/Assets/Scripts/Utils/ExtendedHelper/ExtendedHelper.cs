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

    //�鸸������ ���� 0~1
    public static float PPMToFloat(this long src)
    {
        return src * 0.000001f;
    }

    //�鸸������ ���� 0~1
    public static float PPMToFloat(this int src)
    {
        return src * 0.000001f;
    }
}