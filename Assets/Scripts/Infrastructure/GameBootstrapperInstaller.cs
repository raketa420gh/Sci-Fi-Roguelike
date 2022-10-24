using UnityEngine;
using Zenject;

public class GameBootstrapperInstaller : MonoInstaller
{
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        var gameBootstrapperPrefab = Resources.Load<GameBootstrapper>("Prefabs/Infrastructure/GameBootstrapper");
        
        Container
            .BindInstance(Instantiate(gameBootstrapperPrefab))
            .AsSingle()
            .NonLazy();
    }
}