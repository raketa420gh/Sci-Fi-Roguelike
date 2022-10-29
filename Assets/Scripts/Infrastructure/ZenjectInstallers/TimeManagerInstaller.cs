using Zenject;

public class TimeManagerInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container
            .Bind<ITimeManager>()
            .To<TimeManager>()
            .AsSingle()
            .NonLazy();
    }
}