using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rotatable))]
[RequireComponent(typeof(Weapon))]

public class PlayerWeaponSegment : MonoBehaviour
{
    private IRotatable _rotatable;
    private IWeapon _weapon;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public IRotatable Rotatable => _rotatable;

    private void Awake()
    {
        _rotatable = GetComponent<IRotatable>();
        _weapon = GetComponent<IWeapon>();
    }

    public void StartFire()
    {
        _weapon.StartFire();
    }

    public void StopFire()
    {
        _weapon.StopFire();
    }
}