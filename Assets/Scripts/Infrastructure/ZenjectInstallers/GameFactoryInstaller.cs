using Zenject;

public class GameFactoryInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container
            .Bind<IGameFactory>()
            .To<GameFactory>()
            .AsSingle()
            .NonLazy();
    }
}