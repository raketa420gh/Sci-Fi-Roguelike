using UnityEngine;
using Zenject;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";

    private IPersistentProgressService _progressService;
    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
        _progressService = progressService;
        _gameFactory = gameFactory;
    }
    
    public void SaveProgress()
    {
        foreach (var progressWriter in _gameFactory.ProgressWriters)
            progressWriter.SaveProgress(_progressService.Progress);
        
        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress() => 
        PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
}