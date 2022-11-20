using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class KickerEnemyShootingSystem : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private Transform _player;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        _player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!_healthSystem.isDeath)
        {
            float movementSpeed = 2f;

            transform.position = Vector3.MoveTowards(transform.position,
                                        _player.position,
                                        movementSpeed * Time.deltaTime);
        }
        else
        {
            _collisionSystem.FallDown();
        }
    }
}