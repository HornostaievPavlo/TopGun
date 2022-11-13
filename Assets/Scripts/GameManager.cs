using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Scene reloading
    public bool isGameOver = false;

    private float restartDelay;

    // Time

    private float _fixedDeltaTime;

    private void Awake()
    {
        _fixedDeltaTime = Time.fixedDeltaTime;
    }

    public void EndGame()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Invoke("RestartLevel", restartDelay);
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
