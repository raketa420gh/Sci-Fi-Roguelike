using UnityEngine;

public class InteractableDestroyObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}