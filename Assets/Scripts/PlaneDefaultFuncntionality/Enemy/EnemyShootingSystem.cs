using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class EnemyShootingSystem : MonoBehaviour
{
    private ShootingSystem _shootingSystem;

    private float _ammoFireElapsedTime = 0f;

    [HideInInspector] public float _ammoFireDelay;

    [HideInInspector] public Transform _ammoSpawner;

    [HideInInspector] public GameObject _ammoPrefab;

    public float time;
    public float shootingRate;

    private void Start()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _shootingSystem = GetComponent<ShootingSystem>();
    }

    private void Update()
    {
        Fire();

        enabled = _shootingSystem.enabled;
    }

    private void Fire()
    {
        InvokeRepeating("FireShot", time, shootingRate);
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