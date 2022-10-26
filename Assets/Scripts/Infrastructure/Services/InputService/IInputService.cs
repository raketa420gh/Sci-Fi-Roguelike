using UnityEngine;

public interface IInputService
{
    Vector2 AxisMove { get; }
    Vector2 AxisAim { get; }
    bool Interacted { get; }
}