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

            case PlaneType.Bomber_Enemy:

                break;

        }
    }

    private void PlayerFire()
    {
        Debug.Log("Switch to PLAYER shooting mode");

        float bombFireDelay = 3f;
        float bulletFireDelay = 0.15f;

        _bulletFireElapsedTime += Time.deltaTime;
        _bombFireElapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1)) //&& _bombFireElapsedTime > bombFireDelay)
        {
            _bombFireElapsedTime = 0;

            Instantiate(_bombPrefab, _ammoSpawner.position, transform.rotation);
        }
        if (Input.GetKey(KeyCode.Mouse0) && _bulletFireElapsedTime > bulletFireDelay)
        {
            _bulletFireElapsedTime = 0;

            Instantiate(_bulletPrefab, _ammoSpawner.position, transform.rotation);
        }
    }

    private void ShooterEnemyFire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.LogWarning("Switch to ENEMY shooting mode");

            Instantiate(_bombPrefab, _ammoSpawner.position, transform.rotation);

        }
    }

    private void BomberEnemyFire()
    {

    }
}