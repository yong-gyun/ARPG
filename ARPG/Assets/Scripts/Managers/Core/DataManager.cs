using UnityEngine;

public partial class DataManager
{
    public float GetConstValue(Define.ConstDefType constDefType)
    {
        var script = GetConstValueScripts.Find(x => x.ConstType == constDefType);
        if (script == null)
            return 0f;

        return script.Value;
    }
}