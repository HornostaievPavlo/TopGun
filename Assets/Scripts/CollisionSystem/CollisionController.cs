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

    [Range(0, 1)] public float _timeScaleVar;

    private float _fixedDeltaTime;

    private void Awake()
    {
        _fixedDeltaTime = Time.fixedDeltaTime;

        _enemyController = GetComponent<EnemyController>();
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

    private void ApplyBulletDamage() // explosion for bomb
    {
        //Debug.LogWarning("Bullet hit");

        _enemyController.health -= 1;

        // add particle playing after half hp left
    }

    private void ApplyBombDamage()
    {
        //Debug.LogWarning("Bomb hit");

        _enemyController.DestroyWithBomb();

        _enemyController.health = 0;

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
        float delay = 3;

        yield return new WaitForSecondsRealtime(delay);

        SetTimeScale(1);
    }
}