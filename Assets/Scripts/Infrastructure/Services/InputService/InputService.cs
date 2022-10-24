using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";

    public abstract Vector2 Axis { get; }
    public abstract bool GetTouchHold { get; }
    public abstract bool GetTouchDown { get; }
    public abstract bool GetTouchUp { get; }

    protected static Vector2 GetSimpleInputAxis() => 
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    
    protected static bool GetMouseButton(int button) => 
        Input.GetMouseButton(button);

    protected static bool GetMouseButtonDown(int button) =>
        Input.GetMouseButtonDown(button);

    protected static bool GetMouseButtonUp(int button) =>
        Input.GetMouseButtonUp(button);
}