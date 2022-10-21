public class BootstrapGameState : GameState
{
    private readonly Game _game;
    private readonly SceneLoader _sceneLoader;
    
    public BootstrapGameState(Game game, SceneLoader sceneLoader) : base(game)
    {
        _game = game;
        _sceneLoader = sceneLoader;
    }

    public override void Enter()
    {
        base.Enter();
        
        _sceneLoader.LoadScene(SceneNames.Initial, OnInitialSceneLoaded);
    }

    private void OnInitialSceneLoaded()
    {
        _game.StateMachine.ChangeState(_game.LoadLevelState);
    }
}