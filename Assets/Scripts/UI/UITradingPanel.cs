using UnityEngine;
using UnityEngine.UI;

public class UITradingPanel : UIPanel
{
    [SerializeField] private Button _closeButton;

    public Button CloseButton => _closeButton;
}