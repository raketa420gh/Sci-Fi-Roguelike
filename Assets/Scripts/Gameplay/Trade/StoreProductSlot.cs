using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreProductSlot : MonoBehaviour
{
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _buyButton;
    
    private InventoryItemData _itemData;

    public Button BuyButton => _buyButton;
    public InventoryItemData ItemData => _itemData;

    public void Setup(InventoryItemData itemData)
    {
        _itemData = itemData;
        _productImage.sprite = _itemData.SpriteIcon;
        _costText.text = _itemData.Cost.ToString();
    }
}