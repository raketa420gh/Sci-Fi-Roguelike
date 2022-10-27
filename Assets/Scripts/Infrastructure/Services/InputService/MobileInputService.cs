﻿using UnityEngine;

public class MobileInputService : InputService
{
    public override Vector2 AxisMove => GetSimpleInputAxisMove();
    public override Vector2 AxisAim => GetSimpleInputAxisAim();
    public override bool IsInteractButtonDown => GetInteractButtonDown();
    public override bool IsInventoryButtonDown => GetInventoryButtonDown();
}