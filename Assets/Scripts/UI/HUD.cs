using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIPlayerInput _uiPlayerInput;
    [SerializeField] private UIPlayerInventory _uiPlayerInventory;

    private IInputService _inputService;
    private bool _isInventoryPanelActive;
    
    public UIPlayerInput UIPlayerInput => _uiPlayerInput;
    public UIPlayerInventory UIPlayerInventory => _uiPlayerInventory;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Update()
    {
        if(_inputService.IsInventoryButtonDown)
            ToggleInventory(!_isInventoryPanelActive);
    }

    public void ToggleInventory(bool isActive)
    {
        _uiPlayerInventory.SetActive(isActive);
        _uiPlayerInput.SetActive(!isActive);
        _isInventoryPanelActive = isActive;
    }
}
