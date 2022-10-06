using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyCollisionController : MonoBehaviour
{
    //particles to implement from the beginning
    //(have prefab with old particles)

    //[SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;

    [SerializeField] private GameManager _gameManager;

    [Range(0, 1)] public float _timeScaleVar;

    private EnemyController _enemyController;

    private void Awake()
    {
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

    private void ApplyBulletDamage()
    {
        //Debug.LogWarning("Bullet hit");

        _enemyController._health -= 1;
    }

    private void ApplyBombDamage()
    {
        //Debug.LogWarning("Bomb hit");

        _enemyController.ExplodeEnemy();

        _gameManager.SetTimeScale(_timeScaleVar);

        StartCoroutine(_gameManager.ResetTimeScale(2));
    }
}