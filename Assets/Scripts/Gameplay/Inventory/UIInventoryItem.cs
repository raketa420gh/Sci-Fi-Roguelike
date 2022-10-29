using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textAmount;
    
    public IInventoryItem Item { get; private set; }

    public void Refresh(IInventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            Clear();
            return;
        }

        Item = slot.Item;
        _imageIcon.sprite = Item.Info.SpriteIcon;
        
        _imageIcon.gameObject.SetActive(true);
        var textAmountEnabled = slot.Amount > 0;
        _textAmount.gameObject.SetActive(textAmountEnabled);

        if (textAmountEnabled)
            _textAmount.text = slot.Amount.ToString();
    }

    private void Clear()
    {
        _imageIcon.gameObject.SetActive(false);
        _textAmount.gameObject.SetActive(false);
    }
}