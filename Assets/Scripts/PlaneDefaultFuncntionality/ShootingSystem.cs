using UnityEngine;

[RequireComponent(typeof(GameEntity))]
public class ShootingSystem : MonoBehaviour
{
    private PlaneType _planeType;

    public Transform _bulletSpawner;

    public Transform _bombSpawner;

    public GameObject _bulletPrefab;

    public GameObject _bombPrefab;

    private void Start()
    {
        _planeType = GetComponent<GameEntity>()._type;

        AssignShootingSystem();
    }

    private void AssignShootingSystem()
    {
        switch (_planeType)
        {
            case PlaneType.Player:

                Debug.Log("We have a player");

                gameObject.AddComponent<PlayerShootingSystem>();

                break;

            case PlaneType.Shooter_Enemy:

                var _shootingSystem = gameObject.AddComponent<EnemyShootingSystem>();

                _shootingSystem._ammoSpawner = _bulletSpawner;

                _shootingSystem._ammoPrefab = _bulletPrefab;

                _shootingSystem._ammoFireDelay = 0.3f;

                break;

            case PlaneType.Bomber_Enemy:

                var _bombingSystem = gameObject.AddComponent<EnemyShootingSystem>();

                _bombingSystem._ammoSpawner = _bombSpawner;

                _bombingSystem._ammoPrefab = _bombPrefab;

                _bombingSystem._ammoFireDelay = 3.0f;

                break;

            case PlaneType.Kicker_Enemy:

                gameObject.AddComponent<KickerEnemyShootingSystem>();

                break;
        }
    }
}