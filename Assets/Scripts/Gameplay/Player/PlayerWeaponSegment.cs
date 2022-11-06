using UnityEngine;

[RequireComponent(typeof(Rotatable))]
[RequireComponent(typeof(Weapon))]

public class PlayerWeaponSegment : MonoBehaviour
{
    [SerializeField] private WeaponSegmentType _weaponSegmentType;
    
    private IRotatable _rotatable;
    private IWeapon[] _weapons;

    public WeaponSegmentType Type => _weaponSegmentType;

    public IRotatable Rotatable => _rotatable;

    private void Awake()
    {
        _rotatable = GetComponent<IRotatable>();
        _weapons = GetComponents<IWeapon>();
    }

    public void StartFire()
    {
        foreach (var weapon in _weapons)
            weapon.StartFire();
    }

    public void StopFire()
    {
        foreach (var weapon in _weapons)
            weapon.StopFire();
    }
}