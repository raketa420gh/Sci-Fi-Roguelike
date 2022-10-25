using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class IntractablePhysicalObject : MonoBehaviour, IInteractable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(IInteractionSource source)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
        
        source.Interact();
    }
}