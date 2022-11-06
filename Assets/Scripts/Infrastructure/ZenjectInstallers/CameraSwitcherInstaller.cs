using Zenject;

public class CameraSwitcherInstaller : MonoInstaller
{
    public CameraController cameraController;
    
    public override void InstallBindings() => Bind();
    
    private void Bind()
    {
        Container
            .BindInstance(cameraController)
            .AsSingle()
            .NonLazy();
    }
}