using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    public const string ProgressKey = "Progress";
    
    public void SaveProgress()
    {
        
    }

    public PlayerProgress LoadProgress() => 
        PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
}