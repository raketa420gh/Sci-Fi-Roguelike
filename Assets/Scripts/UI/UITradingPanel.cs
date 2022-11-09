using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITradingPanel : UIPanel
{
    public event Action<StoreProductSlot> OnBuyButtonClicked;
    
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _productsParent;
    [SerializeField] private StoreProductSlot _productSlotPrefab;

    private List<StoreProductSlot> _productSlots = new List<StoreProductSlot>();

    public Button CloseButton => _closeButton;

    private void OnDestroy()
    {
        foreach (var productSlot in _productSlots)
            productSlot.BuyButton.onClick.RemoveListener(() => { Buy(productSlot); });;
    }

    public void SetupProductList(List<InventoryItemData> productList)
    {
        foreach (var itemData in productList)
        {
            var productSlot = Instantiate(_productSlotPrefab, _productsParent);
            productSlot.Setup(itemData);
            _productSlots.Add(productSlot);
            productSlot.BuyButton.onClick.AddListener(() => { Buy(productSlot); });
        }
    }

    private void Buy(StoreProductSlot productSlot)
    {
        OnBuyButtonClicked?.Invoke(productSlot);
    }
}