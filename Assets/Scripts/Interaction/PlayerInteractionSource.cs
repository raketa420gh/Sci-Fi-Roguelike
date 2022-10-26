using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class PlayerInteractionSource : MonoBehaviour, IInteractionSource
{
    public event Action<bool> OnAvailable;

    private SphereCollider _collider;
    private IInteractable _currentInteractable;

    private void Awake() => 
        _collider = GetComponent<SphereCollider>();

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            if (_currentInteractable == null)
            {
                _currentInteractable = interactable;
                OnAvailable?.Invoke(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        
        if (interactable != null)
        {
            if (interactable == _currentInteractable)
            {
                _currentInteractable = null;
                OnAvailable?.Invoke(false);
            }
        }
    }

    public void Interact()
    {
        _currentInteractable?.Interact();
        _currentInteractable = null;
        OnAvailable?.Invoke(false);
    }
}