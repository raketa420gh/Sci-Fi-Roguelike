public interface IWeapon
{
    float FiringRate { get; }

    void StartFire();
    void StopFire();
}