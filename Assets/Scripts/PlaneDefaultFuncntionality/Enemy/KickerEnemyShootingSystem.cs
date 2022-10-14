using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class KickerEnemyShootingSystem : MonoBehaviour
{
    private EnemyMovementSystem _enemyMovementSystem;

    private HealthSystem _healthSystem;

    private Transform _player;

    void Start()
    {
        Debug.Log("hello kicker");

        _enemyMovementSystem = GetComponent<EnemyMovementSystem>();

        _enemyMovementSystem.enabled = false;

        _healthSystem = GetComponent<HealthSystem>();

        _player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!_healthSystem._isDeath)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                        _player.position,
                                        2 * Time.deltaTime);
        }
    }
}