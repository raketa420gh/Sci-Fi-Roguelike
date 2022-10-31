using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIPlayerInput _uiInputPanel;
    [SerializeField] private UIPlayerInventory _uiInventoryPanel;
    [SerializeField] private UIPlayerEquipment _uiEquipmentPanel;

    private Canvas _hudCanvas;
    private IInputService _inputService;
    private bool _isInventoryPanelActive;
    
    public UIPlayerInput UIInputPanel => _uiInputPanel;
    public UIPlayerInventory UIInventoryPanel => _uiInventoryPanel;
    public UIPanel UIEquipmentPanel => _uiEquipmentPanel;

    [Inject]
    public void Construct(IInputService inputService) => 
        _inputService = inputService;

    private void Awake() => 
        _hudCanvas = GetComponent<Canvas>();

    private void Update()
    {
        if(_inputService.IsInventoryButtonDown)
            ToggleInventory(!_isInventoryPanelActive);
    }

    public void ToggleInventory(bool isActive)
    {
        _uiInventoryPanel.SetActive(isActive);
        _uiEquipmentPanel.SetActive(isActive);
        _uiInputPanel.SetActive(!isActive);
        
        _isInventoryPanelActive = isActive;
    }
}
