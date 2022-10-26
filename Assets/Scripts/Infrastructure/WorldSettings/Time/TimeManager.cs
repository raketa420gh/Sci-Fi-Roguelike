using UnityEngine;

public class TimeManager : ITimeManager
{
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
        
        Time.fixedDeltaTime = value * 0.02f;
    }

    public void SetTimeScaleByDefault() => SetTimeScale(1);
}