using UnityEngine;

public class MobileInputService : InputService
{
    public override Vector2 Axis => GetSimpleInputAxis();

    public override bool GetTouchHold => GetMouseButton(0);
    public override bool GetTouchDown => GetMouseButtonDown(0);
    public override bool GetTouchUp => GetMouseButtonUp(0);
}