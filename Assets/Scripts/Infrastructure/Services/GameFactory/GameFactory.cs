using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private IAssetProvider _assetProvider;

    public List<ISavableProgressReader> ProgressReaders { get; } = new List<ISavableProgressReader>();
    public List<ISavableProgress> ProgressWriters { get; } = new List<ISavableProgress>();

    [Inject]
    public void Construct(IAssetProvider assetProvider) => 
        _assetProvider = assetProvider;

    public Player CreatePlayerCharacter(Vector3 position)
    {
        var playerCharacterPrefab = _assetProvider.Instantiate("Prefabs/Units/PlayerCharacter", position);

        var player = playerCharacterPrefab.GetComponent<Player>();

        RegisterProgressWatchers(playerCharacterPrefab);

        return player;
    }

    public void CreateHUD()
    {
        var hud = _assetProvider.Instantiate("Prefabs/UI/HUD");
    }

    public void Cleanup()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
    }

    private void RegisterProgressWatchers(GameObject playerCharacterPrefab)
    {
        foreach (ISavableProgressReader progressReader in playerCharacterPrefab
                     .GetComponentsInChildren<ISavableProgressReader>())
            Register(progressReader);
    }

    private void Register(ISavableProgressReader progressReader)
    {
        if (progressReader is ISavableProgress progressWriter)
            ProgressWriters.Add(progressWriter);
        
        ProgressReaders.Add(progressReader);
    }
}