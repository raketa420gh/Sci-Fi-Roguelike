using UnityEngine;
using Zenject;

public class Trader : MonoBehaviour, ITrader
{
    [SerializeField] private string _tradingPanelPath;

    private IGameFactory _factory;
    private UITradingPanel _tradingPanel;
    private IBuyer _currentBuyer;

    [Inject]
    public void Construct(IGameFactory factory)
    {
        _factory = factory;
    }

    public void Interact(IInteractionSource interactionSource)
    {
    }

    public void FinishTrading()
    {
        _tradingPanel.SetActive(false);
        _currentBuyer.FinishTrading();
        _currentBuyer = null;
    }

    public void StartTrading(IBuyer buyer)
    {
        _currentBuyer = buyer;
        
        if (!_tradingPanel)
        {
            _tradingPanel = _factory.CreateTradingPanel(_tradingPanelPath);
            _tradingPanel.CloseButton.onClick.AddListener(FinishTrading);
        }
        else
            _tradingPanel.SetActive(true);
    }
}