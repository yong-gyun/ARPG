using Data.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class ExtendedHelper
{
    public static int GetSkillLevelValue(this EffectInfoScript script, int level)
    {
        switch (level)
        {
            case 1:
                return script.Lv1;
            case 2:
                return script.Lv2;
            case 3:
                return script.Lv3;
            case 4:
                return script.Lv4;
            case 5:
                return script.Lv5;
            case 6:
                return script.Lv6;
            case 7:
                return script.Lv7;
            case 8:
                return script.Lv8;
            case 9:
                return script.Lv9;
            case 10:
                return script.Lv10;
        }

        return 0;
    }

    public static Define.CreatureType GetCreatureType(this int templateID)
    {
        return (Define.CreatureType) (templateID / 1000);
    }

    public static int GetCreatureID(this int templateID)
    {
        return templateID % 1000;
    }
}