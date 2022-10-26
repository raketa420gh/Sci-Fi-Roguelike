using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    public override void InstallBindings() =>
        Bind();

    private void Bind()
    {
        Container
            .Bind<SceneLoader>()
            .AsSingle()
            .NonLazy();
    }
}