using System;

[Serializable]
public class WorldData
{
    public string LevelName;

    public WorldData(string levelName)
    {
        LevelName = levelName;
    }
}