using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class GameEntityTesting
{
    GameObject _object = new GameObject("TESTING");

    [UnityTest]
    public IEnumerator GameEntityComponentIsAdding()
    {
        GameEntity entity = _object.AddComponent<GameEntity>();

        yield return null;

        Assert.NotNull(entity);
    }

    [UnityTest]
    public IEnumerator GameEntityComponentIsAssigningType()
    {
        GameEntity entity = _object.AddComponent<GameEntity>();

        entity.type = PlaneType.Player;

        yield return null;

        Assert.AreEqual(PlaneType.Player, _object.GetComponent<GameEntity>().type);
    }
}
