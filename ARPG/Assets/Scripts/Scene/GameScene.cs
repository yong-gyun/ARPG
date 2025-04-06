using UnityEngine;

public class GameScene : BaseScene
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SpawnTest();
        return true;
    }

    public async void SpawnTest()
    {
        await Managers.Object.Spawn(1001);
    }
}
