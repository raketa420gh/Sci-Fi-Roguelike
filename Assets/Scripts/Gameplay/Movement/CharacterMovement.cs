using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private CharacterController _characterController;

    private void Awake() => 
        _characterController = GetComponent<CharacterController>();

    public void Move(Vector3 movementVector)
    {
        _characterController.Move(movementVector * (Time.deltaTime * _movementSpeed));
    }
}