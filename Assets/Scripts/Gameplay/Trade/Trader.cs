using UnityEngine;
using Zenject;

public class Trader : MonoBehaviour, ITrader
{
    [SerializeField] private string _tradingPanelPath;

    private IGameFactory _factory;
    private UITradingPanel _tradingPanel;

    [Inject]
    public void Construct(IGameFactory factory)
    {
        _factory = factory;
    }

    public void Sell(IInventoryItem salableItem, CurrencyStorage currencyStorage, int cost)
    {
        
    }

    public void Interact(IInteractionSource interactionSource)
    {
        StartTrading();
    }

    public void LeaveTrader()
    {
        _tradingPanel.SetActive(false);
    }

    private void StartTrading()
    {
        if (!_tradingPanel)
        {
            _tradingPanel = _factory.CreateTradingPanel(_tradingPanelPath);
            _tradingPanel.CloseButton.onClick.AddListener(LeaveTrader);
        }
        else
            _tradingPanel.SetActive(true);
    }
}