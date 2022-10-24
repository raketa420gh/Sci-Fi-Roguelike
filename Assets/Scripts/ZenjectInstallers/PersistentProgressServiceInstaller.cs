using Zenject;

public class PersistentProgressServiceInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container
            .Bind<IPersistentProgressService>()
            .To<PersistentProgressService>()
            .AsSingle()
            .NonLazy();
    }
}