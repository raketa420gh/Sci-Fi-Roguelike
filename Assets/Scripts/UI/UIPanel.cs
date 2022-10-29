using UnityEngine;

public class UIPanel : MonoBehaviour, IUIPanel
{
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}