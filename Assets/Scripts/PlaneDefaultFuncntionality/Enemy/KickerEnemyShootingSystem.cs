using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class KickerEnemyShootingSystem : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private Transform _player;

    private float _movementSpeed;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        _player = GameObject.Find("Player").transform;

        _movementSpeed = Random.Range(2f, 3f);
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!_healthSystem.isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                        _player.position,
                                        _movementSpeed * Time.deltaTime);
        }
        else
        {
            _collisionSystem.FallDown();
        }
    }
}