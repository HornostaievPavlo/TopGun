using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _fixedDeltaTime;

    private void Awake()
    {
        _fixedDeltaTime = Time.fixedDeltaTime;
    }

    /// <summary>
    /// Sets timescale and fixedDeltaTime
    /// to make slow motion effect
    /// </summary>
    /// <param name="timeScaleValue">Needed timeScale value</param>
    public void SetTimeScale(float timeScaleValue) // only 0 to 1
    {
        Time.timeScale = timeScaleValue;

        Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
    }

    /// <summary>
    /// Resets timeScale and fixedDeltaTime after delay
    /// </summary>
    /// <param name="resetDelay">Amount of realtime seconds before reseting</param>
    /// <returns>WaitForSecondsRealtime</returns>
    public IEnumerator ResetTimeScale(float resetDelay)
    {
        float defaultTimeScale = 1f;

        yield return new WaitForSecondsRealtime(resetDelay);

        SetTimeScale(defaultTimeScale);
    }
}
