using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class PlayerInteractionSource : MonoBehaviour, IInteractionSource, IBuyer
{
    public event Action<bool> OnAvailable;

    public event Action<ITrader> OnTradingStarted;
    public event Action OnTradingFinished;

    private SphereCollider _collider;

    private IInteractable _currentInteractable;

    private void Awake() =>
        _collider = GetComponent<SphereCollider>();

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
            SelectInteractable(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
            if (_currentInteractable == interactable)
                DeselectInteractable();
    }

    public void Interact()
    {
        _currentInteractable?.Interact(this);

        if (_currentInteractable is ITrader trader)
        {
            trader.StartTrading(this);
            OnTradingStarted?.Invoke(trader);
        }
    }

    public void FinishTrading()
    {
        OnTradingFinished?.Invoke();
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