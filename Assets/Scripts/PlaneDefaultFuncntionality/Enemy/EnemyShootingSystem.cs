using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class EnemyShootingSystem : MonoBehaviour
{
    private ShootingSystem _shootingSystem;

    private float _ammoFireElapsedTime;

    public float _ammoFireDelay;

    public Transform _ammoSpawner;

    public GameObject _ammoPrefab;

    public float time;
    public float shootingRate;

    private void Start()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _shootingSystem = GetComponent<ShootingSystem>();

        _ammoFireElapsedTime = 0f;
    }

    private void Update()
    {
        Fire();
        enabled = _shootingSystem.enabled;
    }

    private void Fire()
    {
        InvokeRepeating("FireShot", 1, 10);
    }

    private void FireShot()
    {
        _ammoFireElapsedTime += Time.deltaTime;

        if (_ammoFireElapsedTime > _ammoFireDelay)
        {
            _ammoFireElapsedTime = 0;

            Instantiate(_ammoPrefab, _ammoSpawner.position, transform.rotation);
        }
    }
}