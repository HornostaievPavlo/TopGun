using System.Collections;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    //particles to implement from the begging
    //(have prefab with old particles)

    //[SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;
    //[SerializeField] private ParticleSystem playerFire;

    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("baza");

        AmmoType hitType = other.gameObject.GetComponent<AmmoController>().type;

        // better change to Dict<>
        switch (hitType)
        {
            case AmmoType.Bullet:

                ApplyBulletDamage();
                break;

            case AmmoType.Bomb:

                ApplyBombDamage();
                break;
        }

        Destroy(other.gameObject);
    }

    private void ApplyBulletDamage() // explosion for bomb
    {
        Debug.LogWarning("Bullet hit");

        _enemyController.health -= 1;

        // add particle playing after half hp left


    }

    private void ApplyBombDamage()
    {
        Debug.LogWarning("Bomb hit");

        _enemyController.DestroyWithBomb();

        _enemyController.health = 0;

        Time.timeScale = _enemyController.slowMotion;

        StartCoroutine(TurnOnTimeScale());
    }

    private IEnumerator TurnOnTimeScale()
    {
        // multiply to fit reduced timescale

        yield return new WaitForSeconds(1.5f * _enemyController.slowMotion);

        Time.timeScale = 1;
    }

    //private void UpdateState()
    //{
    //    if (isDeath)
    //    {
    //        _enemyController.enabled = false;

    //        // explosion experiment

    //        foreach (Transform item in children)
    //        {
    //            item.gameObject.AddComponent<BoxCollider>();
    //        }

    //        StartCoroutine(EnableGravity());

    //        isDeath = false;
    //    }

    //    float lowerBorder = Screen.width / -154;

    //    if (transform.position.y < lowerBorder)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private IEnumerator EnableGravity()
    //{
    //    yield return new WaitForSeconds(3f);
    //}
}