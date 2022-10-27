using UnityEngine;

public interface IInputService
{
    Vector2 AxisMove { get; }
    Vector2 AxisAim { get; }
    bool IsInteractButtonDown { get; }
    bool IsInventoryButtonDown { get; }
}