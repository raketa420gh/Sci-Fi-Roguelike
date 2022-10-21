using UnityEngine;

public class Game
{
    public StateMachine StateMachine;
    public BootstrapGameState BootstrapState;
    public LoadLevelGameState LoadLevelState;

    private SceneLoader _sceneLoader;
    private IGameFactory _gameFactory;

    public Game(SceneLoader sceneLoader, IGameFactory gameFactory)
    {
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        
        InitializeStateMachine();
        
        Debug.Log("Game state machine initialized");
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        BootstrapState = new BootstrapGameState(this, _sceneLoader);
        LoadLevelState = new LoadLevelGameState(this, _sceneLoader, _gameFactory);
        
        StateMachine.ChangeState(BootstrapState);
        
        Debug.Log("Game state machine initialized");
    }
}