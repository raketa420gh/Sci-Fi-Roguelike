using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreProductSlot : MonoBehaviour
{
    [SerializeField]private InventoryItemData _itemData;
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _buyButton;

    private void Awake()
    {
        _productImage.sprite = _itemData.SpriteIcon;
        _costText.text = _itemData.Cost.ToString();
    }
}