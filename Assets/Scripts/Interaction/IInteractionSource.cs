using System;

public interface IInteractionSource
{
    event Action<bool> OnAvailable;
    void Interact();
}