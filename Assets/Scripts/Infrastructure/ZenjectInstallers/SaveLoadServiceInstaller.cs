using Zenject;

public class SaveLoadServiceInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container
            .Bind<ISaveLoadService>()
            .To<SaveLoadService>()
            .AsSingle()
            .NonLazy();
    }
}