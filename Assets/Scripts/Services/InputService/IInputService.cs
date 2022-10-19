using UnityEngine;

public interface IInputService
{
    Vector2 Axis { get; }
    bool GetTouchHold { get; }
    bool GetTouchDown { get; }
    bool GetTouchUp { get; }
}