using UnityEngine;

public class UI_Scene : MonoBehaviour
{
    public virtual void Init()
    {
        Managers.UI.RegsiterSceneUI(this);
    }

    public virtual void Clear()
    {
        Managers.UI.UnregsiterSceneUI(this);
    }
}
