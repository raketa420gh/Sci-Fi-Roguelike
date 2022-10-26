using DG.Tweening;
using UnityEngine;

public class Rotatable : MonoBehaviour, IRotatable
{
    public void LookAtSmoothOnlyY(Vector3 targetWorldPosition, float duration)
    {
        var towards = new Vector3(targetWorldPosition.x, transform.position.y, targetWorldPosition.z);
        transform.DOLookAt(towards, duration);
    }
}