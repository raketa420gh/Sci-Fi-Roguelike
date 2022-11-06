using UnityEngine;

public class ActiveState : PlayerState
{
    private readonly StateMachine _stateMachine;
    private readonly IInputService _inputService;
    private readonly IInteractionSource _interactionSource;
    private readonly CharacterMovement _characterMovement;
    private readonly PlayerWeaponSwitcher _weaponSwitcher;
    private readonly Transform _body;
    private readonly CameraController _cameraController;

    public ActiveState(Player player,
        StateMachine stateMachine,
        IInputService inputService,
        IInteractionSource interactionSource, 
        CharacterMovement characterMovement,
        PlayerWeaponSwitcher weaponSwitcher,
        Transform body,
        CameraController cameraController) : base(player)
    {
        _stateMachine = stateMachine;
        _inputService = inputService;
        _interactionSource = interactionSource;
        _characterMovement = characterMovement;
        _weaponSwitcher = weaponSwitcher;
        _body = body;
        _cameraController = cameraController;
    }

    public override void Enter()
    {
        base.Enter();
        
        _cameraController.SetPlayerFollowCamera();
    }

    public override void Update()
    {
        base.Update();
        
        var moveVector = ConvertDirection(_inputService.AxisMove.normalized);
        var aimVector = ConvertDirection(_inputService.AxisAim.normalized);
        var aimPoint = _body.position + aimVector;

        _characterMovement.Move(Physics.gravity);

        if (_weaponSwitcher.Current)
        {
            if (aimVector != Vector3.zero)
            {
                _weaponSwitcher.Current.Rotatable.LookAtSmoothOnlyY(aimPoint, 0.1f);
                _weaponSwitcher.Current.StartFire();
            }
            else
                _weaponSwitcher.Current.StopFire();
        }

        if (moveVector != Vector3.zero)
        {
            _characterMovement.Move(moveVector);
            _body.forward = moveVector;
        }

        if (_inputService.IsInteractButtonDown)
            _interactionSource.Interact();
        
        if (_inputService.IsInventoryButtonDown)
            _stateMachine.ChangeState(_player.EquipmentState);
    }

    private static Vector3 ConvertDirection(Vector2 inputDirection) => 
        new Vector3(inputDirection.x, 0, inputDirection.y);
}