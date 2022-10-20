using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]

public class Player : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;

        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        var inputVector = _inputService.Axis;
        var movementVector = ConvertDirection(inputVector);
        
        movementVector.Normalize();
        movementVector += Physics.gravity;
        
        _characterMovement.Move(movementVector);
        
        if (movementVector!= Vector3.zero)
            transform.forward = movementVector;
    }
    
    private static Vector3 ConvertDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);
}
