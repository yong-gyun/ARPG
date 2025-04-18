using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] private CameraController _cameraController;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SpawnTest();
        return true;
    }

    public async void SpawnTest()
    {
        Creature player = await Managers.Object.Spawn(1001);
        _cameraController.Init(player.transform);
    }
}
