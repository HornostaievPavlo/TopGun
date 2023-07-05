using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerTesting
{
    [UnityTest]
    public IEnumerator GameManagerIsSettingTime()
    {
        GameObject _object = new GameObject("TESTING");

        GameManager gameManager = _object.AddComponent<GameManager>();

        gameManager.SetTimeScale(0.5f);

        yield return null;

        Assert.AreEqual(0.5f, Time.timeScale);
    }

    [UnityTest]
    public IEnumerator GameManagerIsResettingTime()
    {
        GameObject _object = new GameObject("TESTING");

        GameManager gameManager = _object.AddComponent<GameManager>();

        gameManager.ResetTimeScale(0.1f);

        yield return null;

        Assert.AreEqual(1, Time.timeScale);
    }
}
