using UnityEngine;

public class Game
{
    public StateMachine StateMachine;
    public BootstrapGameState BootstrapState;

    private SceneLoader _sceneLoader;

    public Game(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        
        InitializeStateMachine();
        
        Debug.Log("Game state machine initialized");
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        BootstrapState = new BootstrapGameState(this, _sceneLoader);
        
        StateMachine.ChangeState(BootstrapState);
        
        Debug.Log("Game state machine initialized");
    }
}