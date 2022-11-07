using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class PlayerInteractionSource : MonoBehaviour, IInteractionSource
{
    public event Action<bool> OnAvailable;
    public event Action OnTraderInteractionStarted;
    public event Action OnTraderInteractionFinished;

    private SphereCollider _collider;
    private IInteractable _currentInteractable;

    private void Awake() => 
        _collider = GetComponent<SphereCollider>();

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable == null) 
            return;
        
        SelectInteractable(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable == null) 
            return;
        if (_currentInteractable == interactable)
            DeselectInteractable();
    }

    public void Interact()
    {
        _currentInteractable?.Interact(this);
        
        if (_currentInteractable as Trader)
            OnTraderInteractionStarted?.Invoke();
        
        DeselectInteractable();
    }

    private void SelectInteractable(IInteractable interactable)
    {
        _currentInteractable = interactable;
        OnAvailable?.Invoke(true);
    }

    private void DeselectInteractable()
    {
        _currentInteractable = null;
        OnAvailable?.Invoke(false);
    }
}