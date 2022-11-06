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

    [SerializeField] private Transform _body;

    private CharacterMovement _characterMovement;
    private PlayerWeaponSwitcher _weaponSwitcher;
    private IInteractionSource _interactionSource;
    private ICurrencyStorage _currencyStorage;
    private IInputService _inputService;
    private IInventory _inventory;
    private UIInventoryController _uiUIInventoryController;
    private CameraController _cameraController;

    private bool _isInventoryState = false;
    private bool _canControl = true;
    
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
        if (_inputService.IsInventoryButtonDown)
            ToggleInventoryState(!_isInventoryState);
        
        if (!_canControl)
            return;
        
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

    public void SetupInventory(UIInventoryController uiInventoryController)
    {
        _uiUIInventoryController = uiInventoryController;
        _inventory = uiInventoryController.UIInventoryWithSlots.Inventory;

        uiInventoryController.UIInventoryWithSlots.OnWeaponEquipped += OnWeaponEquipped;
    }

    public void SetupCameras(CameraController cameraController)
    {
        _cameraController = cameraController;
        
        _cameraController.PlayerFollowCamera.Follow = _body;
        _cameraController.PlayerFollowCamera.LookAt = _body;
        _cameraController.InventoryCamera.Follow = _body;
        _cameraController.InventoryCamera.LookAt = _body;
        
        _cameraController.SetPlayerFollowCamera();
    }

    public void ToggleInputControls(bool isActive) => 
        _canControl = isActive;

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
        {
            _cameraController.SetInventoryCamera();
            _canControl = false;
        }
        else
        {
            _cameraController.SetPlayerFollowCamera();
            _canControl = true;
        }
    }

    private static Vector3 ConvertDirection(Vector2 inputDirection) => 
        new (inputDirection.x, 0, inputDirection.y);

    private void OnWeaponEquipped(Type weaponType)
    {
        if (weaponType == typeof(WeaponSingleItem))
            _weaponSwitcher.SetWeaponSegment(WeaponSegmentType.Single);
        
        if (weaponType == typeof(WeaponDoubleItem))
            _weaponSwitcher.SetWeaponSegment(WeaponSegmentType.Double);
    }
}