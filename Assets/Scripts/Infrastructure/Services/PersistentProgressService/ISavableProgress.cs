public interface ISavableProgress : ISavableProgressReader
{
    void SaveProgress(PlayerProgress progress);
}