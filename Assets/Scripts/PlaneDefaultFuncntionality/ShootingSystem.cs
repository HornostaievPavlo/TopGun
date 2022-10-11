using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private PlaneType _planeType;

    [SerializeField] private Transform _bulletSpawner;

    [SerializeField] private Transform _bombSpawner;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private GameObject _bombPrefab;

    private float _bulletFireElapsedTime;
    private float _bombFireElapsedTime;

    private void Start()
    {
        _bulletFireElapsedTime = 0f;
        _bombFireElapsedTime = 0f;
    }

    void Update()
    {
        AssignFireType();
    }

    private void AssignFireType()
    {
        switch (_planeType)
        {
            case PlaneType.Player:

                PlayerFire();

                break;

            case PlaneType.Shooter_Enemy:

                ShooterEnemyFire();

                break;
        }
    }

    private void PlayerFire()
    {
        //float bombFireDelay = 3f;
        float bulletFireDelay = 0.15f;

        _bulletFireElapsedTime += Time.deltaTime;
        _bombFireElapsedTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && _bulletFireElapsedTime > bulletFireDelay)
        {
            Debug.Log("Switch to PLAYER BULLET mode");

            _bulletFireElapsedTime = 0;

            Instantiate(_bulletPrefab, _bulletSpawner.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))// && _bombFireElapsedTime > bombFireDelay)
        {
            Debug.Log("Switch to PLAYER BOMB mode");

            _bombFireElapsedTime = 0;

            Instantiate(_bombPrefab, _bombSpawner.position, transform.rotation);
        }
    }

    private void ShooterEnemyFire()
    {
        if (Input.GetKeyDown(KeyCode.F)) InvokeRepeating("ShootBullet", 1, 10);


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
    }

    private void ShootBullet()
    {
        float bulletFireDelay = 0.3f;

        _bulletFireElapsedTime += Time.deltaTime;

        if (_bulletFireElapsedTime > bulletFireDelay)
        {
            Debug.Log("Switch to ENEMY BULLET mode");

            _bulletFireElapsedTime = 0;

            Instantiate(_bulletPrefab, _bulletSpawner.position, transform.rotation);
        }
    }
}