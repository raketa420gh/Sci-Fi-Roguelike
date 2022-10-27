using SimpleInputNamespace;
using UnityEngine;

public class UIPlayerInput : UIPanel
{
    [SerializeField] private ButtonInputUI _interactButton;

    private IInteractionSource _interactionSource;

    private void OnDisable()
    {
        if (_interactionSource != null)
            _interactionSource.OnAvailable -= OnInteractionAvailable;
    }
    
    public void Setup(IInteractionSource interactionSource)
    {
        _interactionSource = interactionSource;

        _interactionSource.OnAvailable += OnInteractionAvailable;
    }

    private void OnInteractionAvailable(bool isAvailable)
    {
        _interactButton.gameObject.SetActive(isAvailable);
    }
}