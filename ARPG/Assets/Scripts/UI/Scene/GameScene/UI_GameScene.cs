using UnityEngine;

public class UI_GameScene : UI_Scene
{
    [SerializeField] private UI_Hud _hud;

    public override void Init()
    {
        base.Init();

        _hud.Init(Managers.Object.Hunter);
    }
}
