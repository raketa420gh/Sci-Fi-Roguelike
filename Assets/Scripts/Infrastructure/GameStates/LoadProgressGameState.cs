public class LoadProgressGameState : GameState
{
    private readonly Game _game;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressGameState(Game game, IPersistentProgressService progressService, ISaveLoadService saveLoadService) : base(game)
    {
        _game = game;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
    }

    public override void Enter()
    {
        base.Enter();
        
        _progressService.Progress = 
            _saveLoadService.LoadProgress() ?? InitializeNewProgress();
        
        _game.StateMachine.ChangeState(_game.LoadLevelState);
    }

    private PlayerProgress InitializeNewProgress() => 
        new PlayerProgress(SceneNames.Sandbox);
}