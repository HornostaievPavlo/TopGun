using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class PlayerShootingSystem : MonoBehaviour
{
    private ShootingSystem _shootingSystem;

    private Transform _bulletSpawner;
    private Transform _bombSpawner;

    private GameObject _bulletPrefab;
    private GameObject _bombPrefab;

    private float _bulletFireElapsedTime;
    private float _bombFireElapsedTime;

    private void Start()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _shootingSystem = GetComponent<ShootingSystem>();

        _bulletSpawner = _shootingSystem.bulletSpawner;

        _bombSpawner = _shootingSystem.bombSpawner;

        _bulletPrefab = _shootingSystem.bulletPrefab;

        _bombPrefab = _shootingSystem.bombPrefab;

        _bulletFireElapsedTime = 0f;

        _bombFireElapsedTime = 0f;
    }

    private void Update()
    {
        Fire();
    }

    public void Fire()
    {
        float bulletFireDelay = 0.15f;
        //float bombFireDelay = 3f;

        _bulletFireElapsedTime += Time.deltaTime;
        _bombFireElapsedTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && _bulletFireElapsedTime > bulletFireDelay)
        {
            _bulletFireElapsedTime = 0;

            Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))// && _bombFireElapsedTime > bombFireDelay)
        {
            _bombFireElapsedTime = 0;

            Instantiate(_bombPrefab, _bombSpawner.position, _bombSpawner.rotation);
        }
    }
}
