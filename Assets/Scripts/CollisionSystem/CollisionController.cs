using System.Collections;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    //particles to implement from the beginning
    //(have prefab with old particles)

    //[SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;

    private EnemyMovementSystem _enemyMovementSystem;

    [Range(0, 1)] public float _timeScaleVar;

    private float _fixedDeltaTime;

    private void Awake()
    {
        _fixedDeltaTime = Time.fixedDeltaTime;

        _enemyMovementSystem = GetComponent<EnemyMovementSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AmmoType hitType = other.gameObject.GetComponent<AmmoController>().type;

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

    private void ApplyBulletDamage() 
    {
        //Debug.LogWarning("Bullet hit");

        _enemyMovementSystem.health -= 1;
    }

    private void ApplyBombDamage()
    {
        //Debug.LogWarning("Bomb hit");

        _enemyMovementSystem.ExplodeEnemy();

        SetTimeScale(_timeScaleVar);

        StartCoroutine(ResetTimeScale());
    }

    /// <summary>
    /// Sets timescale and fixedDeltaTime
    /// to make slow motion effect
    /// </summary>
    /// <param name="timeScale">Needed timeScale value</param>
    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;

        Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
    }

    /// <summary>
    /// Resets timeScale and fixedDeltaTime after delay
    /// </summary>
    /// <returns>WaitForSecondsRealtime</returns>
    private IEnumerator ResetTimeScale()
    {
        float delay = 2;

        yield return new WaitForSecondsRealtime(delay);

        SetTimeScale(1);
    }
}