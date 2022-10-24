using System;

[Serializable]
public class PlayerProgress
{
    public WorldData WorldData;

    public PlayerProgress(string initialLevelName)
    {
        WorldData = new WorldData(initialLevelName);
    }
}