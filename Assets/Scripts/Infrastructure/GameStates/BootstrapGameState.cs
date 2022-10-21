using UnityEngine;

public class BootstrapGameState : GameState
{
    private readonly SceneLoader _sceneLoader;
    
    public BootstrapGameState(Game game, SceneLoader sceneLoader) : base(game)
    {
        _sceneLoader = sceneLoader;
    }

    public override void Enter()
    {
        base.Enter();
        
        _sceneLoader.LoadScene(SceneNames.Initial, EnterLoadedScene);
    }

    private void EnterLoadedScene()
    {
        Debug.Log("Initial scene loaded");
    }
}