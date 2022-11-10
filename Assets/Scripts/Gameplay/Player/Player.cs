using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(PlayerInteractionSource))]
[RequireComponent(typeof(PlayerWeaponSwitcher))]

public class Player : MonoBehaviour
{
    public event Action<Player> OnCreated;
    public event Action<Player> OnDead;
    
    public ActiveState ActiveState;
    public EquipmentState EquipmentState;

    [SerializeField] private Transform _body;
    private CharacterMovement _characterMovement;
    private PlayerWeaponSwitcher _weaponSwitcher;
    private PlayerInteractionSource _interactionSource;
    private ICurrencyStorage _currencyStorage;
    private IInputService _inputService;
    private IInventory _inventory;
    private HUD _hud;
    private CameraController _cameraController;
    private StateMachine _stateMachine;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;

        _characterMovement = GetComponent<CharacterMovement>();
        _interactionSource = GetComponent<PlayerInteractionSource>();
        _weaponSwitcher = GetComponent<PlayerWeaponSwitcher>();

        _interactionSource.OnTradingStarted += OnTradingStarted;
        _interactionSource.OnTradingFinished += OnTradingFinished;
        _interactionSource.OnBought += OnItemBought;
    }

    private void OnEnable()
    {
        _currencyStorage = new CurrencyStorage(1000);
        
        OnCreated?.Invoke(this);
    }

    private void OnDisable()
    {
        _hud.UIEquipmentPanel.UIInventoryWithSlots.OnWeaponEquipped -= OnWeaponEquipped;
        _hud.UIEquipmentPanel.UIInventoryWithSlots.OnWeaponUnequipped -= OnWeaponUnequipped;
        
        OnDead?.Invoke(this);
    }

    private void Update() => 
        _stateMachine.CurrentState.Update();

    private void OnTriggerEnter(Collider other)
    {
        var pickableItem = other.GetComponent<IPickableItem>();
        pickableItem?.Pick(_inventory);
    }

    public void Setup(HUD hud, CameraController cameraController)
    {
        SetupHUD(hud);
        SetupCameras(cameraController);
        SetupInventory();

        InitializeStates();
    }

    private void InitializeStates()
    {
        _stateMachine = new StateMachine();

        ActiveState = new ActiveState(this, _stateMachine, _inputService, _interactionSource, 
            _characterMovement, _weaponSwitcher, _body, _cameraController);
        
        EquipmentState = new EquipmentState(this, _stateMachine, _inputService, _cameraController);
        
        _stateMachine.ChangeState(ActiveState);
    }

    private void SetupHUD(HUD hud)
    {
        _hud = hud;
        hud.UIInputPanel.Setup(_interactionSource);
        hud.UIEquipmentPanel.Setup();
        hud.ToggleEquipmentPanel(false);
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

    private void SetupInventory()
    {
        _inventory = _hud.UIEquipmentPanel.UIInventoryWithSlots.Inventory;

        _hud.UIEquipmentPanel.UIInventoryWithSlots.OnWeaponEquipped += OnWeaponEquipped;
        _hud.UIEquipmentPanel.UIInventoryWithSlots.OnWeaponUnequipped += OnWeaponUnequipped;
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

    private void OnTradingStarted(ITrader trader)
    {
        _hud.UIInputPanel.SetActive(false);
        _hud.UIEquipmentPanel.SetActive(false);
    }

    private void OnTradingFinished()
    {
        _hud.ToggleEquipmentPanel(false);
    }

    private void OnItemBought(IInventoryItem item)
    {
        _inventory.TryToAdd(this, item);
    }
}