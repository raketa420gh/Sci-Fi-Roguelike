using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponSwitcher : MonoBehaviour
{
    [SerializeField] private List<PlayerWeaponSegment> _allWeaponSegments = new List<PlayerWeaponSegment>();
    
    public PlayerWeaponSegment Current { get; private set; }

    private void Awake()
    {
        if (_allWeaponSegments.Count == 0)
            _allWeaponSegments.AddRange(GetComponentsInChildren<PlayerWeaponSegment>());
        
        SetWeaponSegment(_allWeaponSegments[0]);
    }

    public void SetWeaponSegment(WeaponSegmentType type)
    {
        foreach (var segment in _allWeaponSegments.Where(segment => segment.Type == type))
            SetWeaponSegment(segment);
    }

    private void SetWeaponSegment(PlayerWeaponSegment segment)
    {
        DisableAllWeaponSegments();
        segment.transform.rotation = Quaternion.identity;
        segment.gameObject.SetActive(true);
        Current = segment;
    }

    private void DisableAllWeaponSegments()
    {
        foreach (var segment in _allWeaponSegments)
            segment.gameObject.SetActive(false);
    }
}