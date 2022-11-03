using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIPlayerInput _uiInputPanel;
    [SerializeField] private UIPlayerInventory uiInventory;

    private Canvas _hudCanvas;
    private IInputService _inputService;
    private bool _isInventoryPanelActive;
    
    public UIPlayerInput UIInputPanel => _uiInputPanel;
    public UIPlayerInventory UIInventory => uiInventory;

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
        uiInventory.SetActive(isActive);
        _uiInputPanel.SetActive(!isActive);
        
        _isInventoryPanelActive = isActive;
    }
}
