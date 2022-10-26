using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(PlayerInteractionSource))]

public class Player : MonoBehaviour, ISavableProgress
{
    public event Action<Player> OnCreated;
    public event Action<Player> OnDead;

    private CharacterMovement _characterMovement;
    private IInteractionSource _interactionSource;
    private PlayerWeaponSegment _weaponSegment;
    private IInputService _inputService;

    public IInteractionSource InteractionSource => _interactionSource;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;

        _characterMovement = GetComponent<CharacterMovement>();
        _interactionSource = GetComponent<IInteractionSource>();
        _weaponSegment = GetComponentInChildren<PlayerWeaponSegment>();
    }

    private void OnEnable()
    {
        
        OnCreated?.Invoke(this);
    }

    private void Update()
    {
        var moveVector = ConvertDirection(_inputService.AxisMove.normalized);
        var aimVector = ConvertDirection(_inputService.AxisAim.normalized);
        var aimPoint = transform.position + aimVector;

        _characterMovement.Move(Physics.gravity);

        if (aimVector != Vector3.zero)
        {
            _weaponSegment.Rotatable.LookAtSmoothOnlyY(aimPoint, 0.1f);
            _weaponSegment.StartFire();
        }
        else
            _weaponSegment.StopFire();

        if (moveVector != Vector3.zero)
        {
            _characterMovement.Move(moveVector);
            transform.forward = moveVector;
        }

        if (_inputService.Interacted)
        {
            _interactionSource.Interact();
        }
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