using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private PlaneType _planeType;

    [SerializeField] private Transform _ammoSpawner;

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

            Instantiate(_bulletPrefab, _ammoSpawner.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))// && _bombFireElapsedTime > bombFireDelay)
        {
            Debug.Log("Switch to PLAYER BOMB mode");

            _bombFireElapsedTime = 0;

            Instantiate(_bombPrefab, _ammoSpawner.position, transform.rotation);
        }
    }

    private void ShooterEnemyFire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.LogWarning("Switch to ENEMY BOMB mode");

            Instantiate(_bombPrefab, _ammoSpawner.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.LogWarning("Switch to ENEMY BULLET mode");

            Instantiate(_bulletPrefab, _ammoSpawner.position, transform.rotation);
        }
    }
}