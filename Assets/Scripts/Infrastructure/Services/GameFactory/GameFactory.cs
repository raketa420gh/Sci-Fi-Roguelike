using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private IAssetProvider _assetProvider;

    [Inject]
    public void Construct(IAssetProvider assetProvider) => 
        _assetProvider = assetProvider;

    public Player CreatePlayerCharacter(Vector3 position)
    {
        var playerCharacterPrefab = _assetProvider.Instantiate("Prefabs/Units/PlayerCharacter", position);

        var player = playerCharacterPrefab.GetComponent<Player>();

        return player;
    }

    public void CreateHUD()
    {
        var hud = _assetProvider.Instantiate("Prefabs/UI/HUD");
    }
}