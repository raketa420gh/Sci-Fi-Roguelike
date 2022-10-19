using Zenject;

public class CameraSwitcherInstaller : MonoInstaller
{
    public CameraSwitcher CameraSwitcher;
    
    public override void InstallBindings() => Bind();
    
    private void Bind()
    {
        Container
            .BindInstance(CameraSwitcher)
            .AsSingle()
            .NonLazy();
    }
}