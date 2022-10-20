public class Game
{
    public StateMachine StateMachine;
    public BootstrapGameState BootstrapState;

    public Game()
    {
        StateMachine = new StateMachine();

        BootstrapState = new BootstrapGameState(this);
    }
}