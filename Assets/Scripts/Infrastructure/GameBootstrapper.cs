using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private Game _game;
    private SceneLoader _sceneLoader;
    private IGameFactory _gameFactory;
    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;

    [Inject]
    public void Construct(SceneLoader sceneLoader, 
        IGameFactory gameFactory, 
        IPersistentProgressService progressService, 
        ISaveLoadService saveLoadService)
    {
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        
        _game = new Game(_sceneLoader, _gameFactory, _progressService, _saveLoadService);
        
        DontDestroyOnLoad(this);
    }
}