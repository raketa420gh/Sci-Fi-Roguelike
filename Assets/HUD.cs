using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private UIPlayerInput _uiPlayerInput;
    [SerializeField] private UIPlayerInventory _uiPlayerInventory;

    public UIPlayerInput UIPlayerInput => _uiPlayerInput;
    public UIPlayerInventory UIPlayerInventory => _uiPlayerInventory;

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
