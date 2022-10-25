using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]

public class Player : MonoBehaviour, ISavableProgress
{
    private CharacterMovement _characterMovement;
    private PlayerWeaponSegment _weaponSegment;
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;

        _characterMovement = GetComponent<CharacterMovement>();
        _weaponSegment = GetComponentInChildren<PlayerWeaponSegment>();
    }

    private void Update()
    {
        var inputMoveVector = _inputService.AxisMove;
        var inputAimVector = _inputService.AxisAim;
        
        var moveVector = ConvertDirection(inputMoveVector.normalized);
        var aimVector = ConvertDirection(inputAimVector.normalized);
        var aimPoint = transform.position + aimVector;

        _characterMovement.Move(Physics.gravity);

        if (aimVector != Vector3.zero)
        {
            _weaponSegment.Rotatable.LookAtSmoothOnlyY(aimPoint, 0.1f);
            _weaponSegment.StartFire();
        }
        else
        {
            _weaponSegment.StopFire();
        }

        if (moveVector == Vector3.zero) 
            return;
        
        _characterMovement.Move(moveVector);
        transform.forward = moveVector;
    }

    public void SaveProgress(PlayerProgress progress)
    {
        //progress.WorldData.LevelName = SceneManager.GetActiveScene().name;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        
    }

    private static Vector3 ConvertDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);
}