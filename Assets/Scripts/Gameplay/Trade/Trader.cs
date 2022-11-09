using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Trader : MonoBehaviour, ITrader
{
    [SerializeField] private string _tradingPanelPath;
    [SerializeField] private List<InventoryItemData> _productList = new List<InventoryItemData>();

    private IGameFactory _factory;
    private UITradingPanel _tradingPanel;
    private IBuyer _currentBuyer;

    [Inject]
    public void Construct(IGameFactory factory)
    {
        _factory = factory;

        SetupTradingPanel();
    }

    private void OnDestroy()
    {
        _tradingPanel.OnBuyButtonClicked -= OnProductBuy;
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
        
        _tradingPanel.CloseButton.onClick.AddListener(FinishTrading);
        _tradingPanel.SetActive(true);
    }

    private void SetupTradingPanel()
    {
        _tradingPanel = _factory.CreateTradingPanel(_tradingPanelPath);
        _tradingPanel.SetActive(false);

        if (_productList.Count != 0)
            _tradingPanel.SetupProductList(_productList);

        _tradingPanel.OnBuyButtonClicked += OnProductBuy;
    }

    private void OnProductBuy(StoreProductSlot productSlot)
    {
        
    }
}