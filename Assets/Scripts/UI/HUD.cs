using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIPlayerInput _uiInputPanel;
    [SerializeField] private UIInventoryController _uiInventoryController;

    private Canvas _hudCanvas;
    private IInputService _inputService;
    private bool _isInventoryPanelActive;
    
    public UIPlayerInput UIInputPanel => _uiInputPanel;
    public UIInventoryController UIInventoryController => _uiInventoryController;

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
        _uiInventoryController.SetActive(isActive);
        _uiInputPanel.SetActive(!isActive);
        
        _isInventoryPanelActive = isActive;
    }
}