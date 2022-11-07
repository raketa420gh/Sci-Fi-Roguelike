using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class IntractablePhysicalObject : MonoBehaviour, IInteractable
{
    [SerializeField] private float _force = 50f;
    [SerializeField] private Vector3 _vector = Vector3.up;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(IInteractionSource interactionSource)
    {
        _rigidbody.AddForce(_vector * _force, ForceMode.Impulse);
    }
}