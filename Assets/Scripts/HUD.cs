using System;
using UnityEngine;
using Zenject;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIPlayerInput _uiPlayerInput;
    [SerializeField] private UIPlayerInventory _uiPlayerInventory;

    private IInputService _inputService;
    
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
            ShowPlayerInventory();
    }

    public void ShowPlayerInventory()
    {
        _uiPlayerInventory.SetActive(true);
        _uiPlayerInput.SetActive(false);
    }

    public void HidePlayerInventory()
    {
        _uiPlayerInventory.SetActive(false);
        _uiPlayerInput.SetActive(true);
    }
}
