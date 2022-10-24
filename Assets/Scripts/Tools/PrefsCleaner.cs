using UnityEditor;
using UnityEngine;

public class PrefsCleaner
{
    [MenuItem("Tools/ClearPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}