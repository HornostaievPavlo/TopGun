using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class KickerEnemyShootingSystem : MonoBehaviour
{
    private EnemyMovementSystem _enemyMovementSystem;

    private ShootingSystem _shootingSystem;

    public Transform _player;

    public float _movementSpeed;

    void Start()
    {
        Debug.Log("hello kicker");

        _enemyMovementSystem = GetComponent<EnemyMovementSystem>();

        _enemyMovementSystem.enabled = false;

        _shootingSystem = GetComponent<ShootingSystem>();

        _player = _shootingSystem._player;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                                _player.position,
                                                2 * Time.deltaTime);
    }
}