using System;

public interface IInteractionSource
{
    event Action<bool> OnAvailable;
    event Action OnTraderInteractionStarted;
    event Action OnTraderInteractionFinished;
    
    void Interact();
}