using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private Game _game;
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        
        _game = new Game(_sceneLoader);
        
        DontDestroyOnLoad(this);
    }
}