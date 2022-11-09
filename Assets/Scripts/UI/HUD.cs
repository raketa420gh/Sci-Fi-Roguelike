using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIPlayerInput _uiInputPanel;
    [SerializeField] private UIEquipmentPanel _uiEquipmentPanel;

    private Canvas _hudCanvas;
    private IInputService _inputService;
    private bool _isEquipmentPanelActive;
    
    public UIPlayerInput UIInputPanel => _uiInputPanel;
    public UIEquipmentPanel UIEquipmentPanel => _uiEquipmentPanel;

    [Inject]
    public void Construct(IInputService inputService) => 
        _inputService = inputService;

    private void Awake() => 
        _hudCanvas = GetComponent<Canvas>();

    private void Update()
    {
        if(_inputService.IsInventoryButtonDown)
            ToggleEquipmentPanel(!_isEquipmentPanelActive);
    }

    public void ToggleEquipmentPanel(bool isActive)
    {
        _uiEquipmentPanel.SetActive(isActive);
        _uiInputPanel.SetActive(!isActive);
        
        _isEquipmentPanelActive = isActive;
    }
}