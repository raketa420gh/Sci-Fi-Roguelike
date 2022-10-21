using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private Game _game;
    private SceneLoader _sceneLoader;
    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(SceneLoader sceneLoader, IGameFactory gameFactory)
    {
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        
        _game = new Game(_sceneLoader, _gameFactory);
        
        DontDestroyOnLoad(this);
    }
}