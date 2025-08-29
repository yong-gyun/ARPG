using UnityEngine;

public class GameScene : BaseScene
{
    public CameraController GetCameraController => _cameraController;

    [SerializeField] private CameraController _cameraController;


    public override bool Initialized()
    {
        if (base.Initialized() == false)
            return false;

        _cameraController = GameObject.FindObjectOfType<CameraController>();
        Cursor.lockState = CursorLockMode.Locked;
        SpawnTest();
        return true;
    }

    public async void SpawnTest()
    {
        Creature player = await Managers.Object.Spawn(1001);
        _cameraController.Init(player.transform);
    }
}
