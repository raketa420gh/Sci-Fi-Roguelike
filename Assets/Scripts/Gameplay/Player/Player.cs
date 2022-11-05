using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(PlayerInteractionSource))]
[RequireComponent(typeof(PlayerWeaponSwitcher))]

public class Player : MonoBehaviour, ISavableProgress, IBuyer
{
    public event Action<Player> OnCreated;
    public event Action<Player> OnDead;

    private CharacterMovement _characterMovement;
    private PlayerWeaponSwitcher _weaponSwitcher;
    private IInteractionSource _interactionSource;
    private IInventory _inventory;
    private ICurrencyStorage _currencyStorage;
    private IInputService _inputService;
    private CameraSwitcher _cameraSwitcher;

    private bool _isInventoryState = false;

    public IInteractionSource InteractionSource => _interactionSource;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;

        _characterMovement = GetComponent<CharacterMovement>();
        _interactionSource = GetComponent<IInteractionSource>();
        _weaponSwitcher = GetComponent<PlayerWeaponSwitcher>();
    }

    private void OnEnable()
    {
        _currencyStorage = new CurrencyStorage(1000);
        
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
            _weaponSwitcher.Current.Rotatable.LookAtSmoothOnlyY(aimPoint, 0.1f);
            _weaponSwitcher.Current.StartFire();
        }
        else
            _weaponSwitcher.Current.StopFire();

        if (moveVector != Vector3.zero)
        {
            _characterMovement.Move(moveVector);
            transform.forward = moveVector;
        }

        if (_inputService.IsInteractButtonDown)
            _interactionSource.Interact();
        
        if (_inputService.IsInventoryButtonDown)
            ToggleInventoryState(!_isInventoryState);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _weaponSwitcher.SetWeaponSegment(WeaponSegmentType.Single);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            _weaponSwitcher.SetWeaponSegment(WeaponSegmentType.Double);
    }

    private void OnTriggerEnter(Collider other)
    {
        var pickableItem = other.GetComponent<IPickableItem>();
        pickableItem?.Pick(_inventory);
    }

    public void SetupInventory(IInventory inventory) =>
        _inventory = inventory;

    public void SetupCameras(CameraSwitcher cameraSwitcher)
    {
        _cameraSwitcher = cameraSwitcher;

        var playerTransform = transform;
        _cameraSwitcher.PlayerFollowCamera.Follow = playerTransform;
        _cameraSwitcher.PlayerFollowCamera.LookAt = playerTransform;
        _cameraSwitcher.InventoryCamera.Follow = playerTransform;
        _cameraSwitcher.InventoryCamera.LookAt = playerTransform;
        
        _cameraSwitcher.SetPlayerFollowCamera();
    }

    public void Buy(IInventoryItem purchasedItem, ICurrencyStorage currencyStorage, int cost = 1)
    {
        _currencyStorage.ChangeAmount(-cost);
        _inventory.TryToAdd(this, purchasedItem);
    }

    public void SaveProgress(PlayerProgress progress)
    {
    }

    public void LoadProgress(PlayerProgress progress)
    {
    }

    private void ToggleInventoryState(bool isActive)
    {
        _isInventoryState = isActive;
        
        if (isActive)
            _cameraSwitcher.SetInventoryCamera();
        else
            _cameraSwitcher.SetPlayerFollowCamera();
    }

    private static Vector3 ConvertDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);
}