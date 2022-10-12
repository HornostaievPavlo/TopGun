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
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        Debug.Log("Hello from player shooting system");

        _shootingSystem = GetComponent<ShootingSystem>();

        _bulletSpawner = _shootingSystem._bulletSpawner;

        _bombSpawner = _shootingSystem._bombSpawner;

        _bulletPrefab = _shootingSystem._bulletPrefab;

        _bombPrefab = _shootingSystem._bombPrefab;

        _bulletFireElapsedTime = 0f;

        _bombFireElapsedTime = 0f;
    }

    private void Update()
    {
        Fire();
        this.enabled = _shootingSystem.enabled;
    }

    public void Fire()
    {
        float bulletFireDelay = 0.15f;
        //float bombFireDelay = 3f;

        _bulletFireElapsedTime += Time.deltaTime;
        _bombFireElapsedTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && _bulletFireElapsedTime > bulletFireDelay)
        {
            //Debug.Log("Switch to PLAYER BULLET mode");

            _bulletFireElapsedTime = 0;

            Instantiate(_bulletPrefab, _bulletSpawner.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))// && _bombFireElapsedTime > bombFireDelay)
        {
            //Debug.Log("Switch to PLAYER BOMB mode");

            _bombFireElapsedTime = 0;

            Instantiate(_bombPrefab, _bombSpawner.position, transform.rotation);
        }
    }
}
