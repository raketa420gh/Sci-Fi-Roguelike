using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    protected const string HorizontalAim = "HorizontalAim";
    protected const string VerticalAim = "VerticalAim";
    protected const string Interact = "Interact";

    public abstract Vector2 AxisMove { get; }
    public abstract Vector2 AxisAim { get; }
    public abstract bool Interacted { get; }
    
    protected static Vector2 GetSimpleInputAxisMove() => 
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

    protected static Vector2 GetSimpleInputAxisAim() =>
        new Vector2(SimpleInput.GetAxis(HorizontalAim), SimpleInput.GetAxis(VerticalAim));

    protected static bool GetInteractButtonDown() => SimpleInput.GetButtonDown(Interact);
}