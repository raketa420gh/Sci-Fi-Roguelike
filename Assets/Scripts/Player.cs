using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]

public class Player : MonoBehaviour, ISavableProgress
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
        var movementVector = ConvertDirection(inputVector.normalized);

        _characterMovement.Move(Physics.gravity);

        if (movementVector == Vector3.zero) 
            return;
        
        _characterMovement.Move(movementVector);
        transform.forward = movementVector;
    }

    public void SaveProgress(PlayerProgress progress)
    {
        progress.WorldData.LevelName = SceneManager.GetActiveScene().name;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        
    }

    private static Vector3 ConvertDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);
}
