using UnityEngine;

public class Game
{
    public StateMachine StateMachine;
    public BootstrapGameState BootstrapState;
    public LoadProgressGameState LoadProgressState;
    public LoadLevelGameState LoadLevelState;
    public GameLoopGameState GameLoopState;

    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public Game(SceneLoader sceneLoader, 
        IGameFactory gameFactory, 
        IPersistentProgressService progressService, 
        ISaveLoadService saveLoadService)
    {
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _saveLoadService = saveLoadService;

        InitializeStateMachine();
        
        Debug.Log("Game state machine initialized");
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        BootstrapState = new BootstrapGameState(this, _sceneLoader);
        LoadProgressState = new LoadProgressGameState(this, _progressService, _saveLoadService);
        LoadLevelState = new LoadLevelGameState(this, _sceneLoader, _gameFactory);
        GameLoopState = new GameLoopGameState(this);
        
        StateMachine.ChangeState(BootstrapState);
        
        Debug.Log("Game state machine initialized");
    }
}