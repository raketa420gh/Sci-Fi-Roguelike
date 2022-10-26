using Zenject;

public class InputServiceInstaller : MonoInstaller
{
    public override void InstallBindings() =>
        Bind();

    private void Bind()
    {
        Container
            .Bind<IInputService>()
            .To<MobileInputService>()
            .AsSingle()
            .NonLazy();
    }
}