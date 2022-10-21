using Zenject;

public class AssetProviderInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container
            .Bind<IAssetProvider>()
            .To<AssetProvider>()
            .AsSingle()
            .NonLazy();
    }
}