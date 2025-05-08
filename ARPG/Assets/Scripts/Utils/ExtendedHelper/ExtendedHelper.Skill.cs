using Cysharp.Threading.Tasks;
using Data.Contents;
using UnityEngine;

public static partial class ExtendedHelper
{
    public static long GetSkillLevelValue(this SkillInfoScript script, int level)
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

    public async static UniTask<GameObject> CreateSkill(this SkillData skillData)
    {
        Vector3 dir = skillData.GetDir();
        Vector3 pos = skillData.GetPos();
        Vector3 dest = new Vector3(dir.x * pos.x, pos.y, dir.z * pos.z);

        Quaternion rot = Quaternion.LookRotation(dir);
        await UniTask.Delay(skillData.actionData.delay);


        var origin = skillData.actionData.skillObject;
        if (origin == null)
            return null;

        GameObject go = Managers.Resource.Instantiate(origin, pos, rot);
        go.name = origin.name;
        return go;
    }
}