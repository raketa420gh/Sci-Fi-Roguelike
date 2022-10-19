using UnityEngine;
using Zenject;

public class ScreenFaderInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();
    
    private void Bind()
    {
        var screenFaderPrefab = Resources.Load<ScreenFader>("ScreenFader");
        
        Container
            .BindInstance(Instantiate(screenFaderPrefab))
            .AsSingle()
            .NonLazy();
    }
}