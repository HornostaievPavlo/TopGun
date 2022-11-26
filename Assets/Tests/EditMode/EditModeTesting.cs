using NUnit.Framework;
using UnityEngine;

public class EditModeTesting
{
    [Test]
    public void SlowMotionIsNotEnabledOnStart()
    {
        Assert.AreEqual(1, Time.timeScale);
    }

    [Test]
    public void FixedDeltaTimeIsSetToDefault()
    {
        Assert.AreEqual(0.02f, Time.fixedDeltaTime);
    }

    [Test]
    public void NoInstancesAreDeadAtTheBeginning()
    {
        GameObject _object = new GameObject();

        HealthSystem healthSystem = _object.AddComponent<HealthSystem>();

        Assert.IsFalse(healthSystem.isDead);
    }

    [Test]
    public void EntityTypeIsNotNull()
    {
        GameObject _object = new GameObject();

        GameEntity entity = _object.AddComponent<GameEntity>();

        Assert.NotNull(entity.type);
    }

    [Test]
    public void EnemyIsNotAssigningMovePointAutomatically()
    {
        GameObject _object = new GameObject();

        EnemyMovementSystem entity = _object.AddComponent<EnemyMovementSystem>();

        Assert.IsNull(entity._movePointsParent);
    }
}
