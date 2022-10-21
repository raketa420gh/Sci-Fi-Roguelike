using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class LoadLevelGameState : GameState
{
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    
    public LoadLevelGameState(Game game, SceneLoader sceneLoader, IGameFactory gameFactory) : base(game)
    {
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
    }

    public override void Enter()
    {
        base.Enter();
        
        _sceneLoader.LoadScene(SceneNames.Sandbox, OnSandboxSceneLoaded);
    }

    private void OnSandboxSceneLoaded()
    {
        var player = _gameFactory.CreatePlayerCharacter(Vector3.up);

        var cinemachineVirtualCameraObject = Object.FindObjectOfType(typeof(CinemachineVirtualCamera));
        var cinemachineVirtualCamera = cinemachineVirtualCameraObject.GetComponent<CinemachineVirtualCamera>();

        cinemachineVirtualCamera.Follow = player.transform;
        cinemachineVirtualCamera.LookAt = player.transform;

        _gameFactory.CreateHUD();
    }
}