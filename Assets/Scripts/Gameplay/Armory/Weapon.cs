using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Shell _shell;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _firingRate = 1f;
    
    public float FiringRate => _firingRate;
    
    private bool _isShooting;
    private float _shootingTimer;

    private void Update()
    {
        _shootingTimer += Time.deltaTime;

        if (!_isShooting) 
            return;
        
        if (_shootingTimer >= _firingRate)
        {
            _shootingTimer = 0f;
            Shoot();
        }
    }

    public void StartFire() => _isShooting = true;

    public void StopFire() => _isShooting = false;

    private void Shoot()
    {
        var projectile = Instantiate(_shell, _muzzle.position, transform.rotation);
    }
}