using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoadLevelGameState : GameState
{
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;

    public LoadLevelGameState(Game game, 
        SceneLoader sceneLoader, 
        IGameFactory gameFactory,
        IPersistentProgressService progressService) : base(game)
    {
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _progressService = progressService;
    }

    public override void Enter()
    {
        base.Enter();
        
        _sceneLoader.LoadScene(_progressService.Progress.WorldData.LevelName, OnGameSceneLoaded);
    }

    private void OnGameSceneLoaded()
    {
        InitGameWorld();
        InformProgressReaders();
    }

    private void InformProgressReaders()
    {
        foreach (var progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
        var player = _gameFactory.CreatePlayerCharacter(Vector3.up);
        var cameraController = _gameFactory.CreateCameraController();
        var hud = _gameFactory.CreateHUD();
        
        SetupCameraController(cameraController);
        SetupHUD(player, hud);
        
        player.Setup(hud.UIInventoryController, cameraController);
    }

    private void SetupCameraController(CameraController cameraController)
    {
        cameraController.transform.parent = Object.FindObjectOfType(typeof(CamerasParentOnScene)).GameObject().transform;
    }

    private void SetupHUD(Player player, HUD hud)
    {
        hud.transform.parent = Object.FindObjectOfType(typeof(UIParentOnScene)).GameObject().transform;
        hud.UIInputPanel.Setup(player.InteractionSource);
        hud.UIInventoryController.Setup();
        hud.ToggleInventory(false);
    }
}