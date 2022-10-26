using UnityEngine;

public interface IRotatable
{
    void LookAtSmoothOnlyY(Vector3 targetWorldPosition, float duration);
}