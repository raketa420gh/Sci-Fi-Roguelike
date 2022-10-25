using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Shell : MonoBehaviour, IProjectile
{
    public event Action<int> OnDamageInflicted;
    
    [SerializeField] [Min(0)] private int _damage = 1;
    [SerializeField] [Min(0)] private float _speed = 100f;
    [SerializeField] [Min(0)] private float _lifeTime = 5f;

    private Rigidbody _rigidbody;

    private void Awake() => 
        _rigidbody = GetComponent<Rigidbody>();

    private void OnEnable() => 
        Destroy(gameObject, _lifeTime);

    private void FixedUpdate() => 
        _rigidbody.velocity = transform.forward * _speed;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();

        if (health)
            health.ChangeHealth(-_damage);

        Destroy(gameObject);
    }

    private void InflictDamage() => 
        OnDamageInflicted?.Invoke(_damage);
}