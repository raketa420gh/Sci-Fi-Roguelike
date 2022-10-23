using UnityEngine;

public static class Extensions
{
    public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
}