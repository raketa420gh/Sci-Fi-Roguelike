using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
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

    public CameraController CreateCameraController()
    {
        var cameraControllerPrefab = _assetProvider.Instantiate("Prefabs/Camera/StateDrivenCamera");
        cameraControllerPrefab.transform.parent = Object.FindObjectOfType(typeof(CamerasParentOnScene)).GameObject().transform;
        var cameraSwitcher = cameraControllerPrefab.GetComponent<CameraController>();

        return cameraSwitcher;
    }

    public HUD CreateHUD()
    {
        var hudPrefab = _assetProvider.Instantiate("Prefabs/UI/HUD");
        hudPrefab.transform.parent = Object.FindObjectOfType(typeof(UIParentOnScene)).GameObject().transform;
        var hud = hudPrefab.GetComponent<HUD>();

        return hud;
    }

    public UITradingPanel CreateTradingPanel(string path)
    {
        var sellerPanelPrefab = _assetProvider.Instantiate(path);
        sellerPanelPrefab.transform.parent = Object.FindObjectOfType(typeof(UIParentOnScene)).GameObject().transform;
        var sellerPanel = sellerPanelPrefab.GetComponent<UITradingPanel>();

        return sellerPanel;
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