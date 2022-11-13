using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    private PlaneType _planeType;

    public Transform bulletSpawner;

    public Transform bombSpawner;

    public GameObject bulletPrefab;

    public GameObject bombPrefab;

    private void Start()
    {
        _planeType = GetComponent<GameEntity>().type;

        AssignShootingSystem();
    }

    private void AssignShootingSystem()
    {
        switch (_planeType)
        {
            case PlaneType.Player:

                gameObject.AddComponent<PlayerShootingSystem>();

                break;

            case PlaneType.Shooter_Enemy:

                var _shootingSystem = gameObject.AddComponent<EnemyShootingSystem>();

                _shootingSystem._ammoSpawner = bulletSpawner;

                _shootingSystem._ammoPrefab = bulletPrefab;

                _shootingSystem._ammoFireDelay = 0.75f;

                break;

            case PlaneType.Bomber_Enemy:

                var _bombingSystem = gameObject.AddComponent<EnemyShootingSystem>();

                _bombingSystem._ammoSpawner = bombSpawner;

                _bombingSystem._ammoPrefab = bombPrefab;

                _bombingSystem._ammoFireDelay = 4.0f;

                break;

            case PlaneType.Kicker_Enemy:

                gameObject.AddComponent<KickerEnemyShootingSystem>();

                break;
        }
    }
}