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
}