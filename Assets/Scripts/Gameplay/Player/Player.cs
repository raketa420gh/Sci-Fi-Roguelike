using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(PlayerInteractionSource))]
[RequireComponent(typeof(PlayerWeaponSwitcher))]

public class Player : MonoBehaviour, IBuyer
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
    private UIInventoryController _uiInventoryController;
    private CameraController _cameraController;
    private StateMachine _stateMachine;
    public ActiveState ActiveState;
    public EquipmentState EquipmentState;
    
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

    private void OnDisable()
    {
        _uiInventoryController.UIInventoryWithSlots.OnWeaponEquipped -= OnWeaponEquipped;
        _uiInventoryController.UIInventoryWithSlots.OnWeaponUnequipped -= OnWeaponUnequipped;
        
        OnDead?.Invoke(this);
    }

    private void Update() => 
        _stateMachine.CurrentState.Update();

    private void OnTriggerEnter(Collider other)
    {
        var pickableItem = other.GetComponent<IPickableItem>();
        pickableItem?.Pick(_inventory);
    }

    public void Setup(UIInventoryController inventoryController, CameraController cameraController)
    {
        SetupInventory(inventoryController);
        SetupCameras(cameraController);
        
        InitializeStates();
    }

    public void Buy(IInventoryItem purchasedItem, ICurrencyStorage currencyStorage, int cost = 1)
    {
        _currencyStorage.ChangeAmount(-cost);
        _inventory.TryToAdd(this, purchasedItem);
    }

    private void InitializeStates()
    {
        _stateMachine = new StateMachine();

        ActiveState = new ActiveState(this, 
            _stateMachine,
            _inputService, 
            _interactionSource, 
            _characterMovement, 
            _weaponSwitcher, 
            _body,
            _cameraController);
        
        EquipmentState = new EquipmentState(this, 
            _stateMachine,
            _inputService,
            _cameraController);
        
        _stateMachine.ChangeState(ActiveState);
    }

    private void SetupInventory(UIInventoryController uiInventoryController)
    {
        _uiInventoryController = uiInventoryController;
        _inventory = uiInventoryController.UIInventoryWithSlots.Inventory;

        _uiInventoryController.UIInventoryWithSlots.OnWeaponEquipped += OnWeaponEquipped;
        _uiInventoryController.UIInventoryWithSlots.OnWeaponUnequipped += OnWeaponUnequipped;
    }

    private void SetupCameras(CameraController cameraController)
    {
        _cameraController = cameraController;
        
        _cameraController.PlayerFollowCamera.Follow = _body;
        _cameraController.PlayerFollowCamera.LookAt = _body;
        _cameraController.InventoryCamera.Follow = _body;
        _cameraController.InventoryCamera.LookAt = _body;
        
        InitializeStates();
    }

    private void OnWeaponEquipped(Type weaponType)
    {
        if (weaponType == typeof(WeaponSingleItem))
            _weaponSwitcher.SetWeaponSegment(WeaponSegmentType.Single);
        
        if (weaponType == typeof(WeaponDoubleItem))
            _weaponSwitcher.SetWeaponSegment(WeaponSegmentType.Double);
    }

    private void OnWeaponUnequipped() => 
        _weaponSwitcher.Disable();
}