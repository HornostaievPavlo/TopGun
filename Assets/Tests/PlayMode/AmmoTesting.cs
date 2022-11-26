using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class AmmoTesting
{
    [UnityTest]
    public IEnumerator AmmoControllerIsAdding()
    {
        GameObject _object = new GameObject("TESTING");

        AmmoController ammoController = _object.AddComponent<AmmoController>();

        ammoController.type = AmmoType.Bullet;

        yield return null;

        Assert.NotNull(ammoController);
    }

    [UnityTest]
    public IEnumerator AmmoControllerIsAssigningType()
    {
        GameObject _object = new GameObject("TESTING");

        AmmoController ammoController = _object.AddComponent<AmmoController>();

        ammoController.type = AmmoType.Bullet;

        yield return null;

        Assert.AreEqual(AmmoType.Bullet, _object.GetComponent<AmmoController>().type);
    }

    [UnityTest]
    public IEnumerator AmmoIsMovingToTheRight()
    {
        GameObject _object = new GameObject("TESTING");

        AmmoController ammoController = _object.AddComponent<AmmoController>();

        _object.transform.position = Vector3.zero;

        ammoController.ammoSpeed = 1;

        ammoController.RIGHT_BOUNDARY = 20;

        ammoController.MoveAmmoItem();

        yield return null;

        Assert.IsTrue(_object.transform.position != Vector3.zero);
    }

    [UnityTest]
    public IEnumerator AmmoIsDestroyed()
    {
        GameObject _object = new GameObject("TESTING");

        AmmoController ammoController = _object.AddComponent<AmmoController>();

        _object.transform.position = Vector3.zero;

        ammoController.ammoSpeed = 1;

        ammoController.RIGHT_BOUNDARY = 2;

        ammoController.MoveAmmoItem();

        yield return new WaitForSeconds(3f);

        Assert.IsTrue(_object == null);
    }
}
