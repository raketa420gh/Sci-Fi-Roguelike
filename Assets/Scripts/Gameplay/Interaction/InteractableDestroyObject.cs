using UnityEngine;

public class InteractableDestroyObject : MonoBehaviour, IInteractable
{
    public void Interact(IInteractionSource interactionSource)
    {
        Destroy(gameObject);
    }
}