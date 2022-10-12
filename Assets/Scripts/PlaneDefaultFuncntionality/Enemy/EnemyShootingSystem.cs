using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class EnemyShootingSystem : MonoBehaviour
{
    private ShootingSystem _shootingSystem;

    private Transform _bulletSpawner;

    private GameObject _bulletPrefab;

    private float _bulletFireElapsedTime;

    // enemy auto shooting
    public float time;
    public float shootingRate;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        Debug.Log("Hello from enemy shooting system");

        _shootingSystem = GetComponent<ShootingSystem>();

        _bulletSpawner = _shootingSystem._bulletSpawner;

        _bulletPrefab = _shootingSystem._bulletPrefab;

        _bulletFireElapsedTime = 0f;
    }

    private void Update()
    {
        Fire();
        this.enabled = _shootingSystem.enabled;
    }

    private void Fire()
    {
        InvokeRepeating("FireBullet", 1, 10);
    }

    private void FireBullet()
    {
        float bulletFireDelay = 0.3f;

        _bulletFireElapsedTime += Time.deltaTime;

        if (_bulletFireElapsedTime > bulletFireDelay)
        {
            _bulletFireElapsedTime = 0;

            Instantiate(_bulletPrefab, _bulletSpawner.position, transform.rotation);
        }
    }
}

// manual testing
//if (Input.GetKeyDown(KeyCode.F))
//{
//    Debug.LogWarning("Switch to ENEMY BULLET mode");

//    Instantiate(_bulletPrefab, _bulletSpawner.position, transform.rotation);
//}

//if (Input.GetKeyDown(KeyCode.G))
//{
//    Debug.LogWarning("Switch to ENEMY BOMB mode");

//    Instantiate(_bombPrefab, _bombSpawner.position, transform.rotation);
//}
