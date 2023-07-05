using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class EnemyShootingSystem : MonoBehaviour
{
    private ShootingSystem _shootingSystem;

    private float _ammoFireElapsedTime = 0f;

    [HideInInspector] public float ammoFireDelay;

    [HideInInspector] public Transform ammoSpawner;

    [HideInInspector] public GameObject ammoPrefab;

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
        StartCoroutine(DelayFire());

        enabled = _shootingSystem.enabled;
    }

    private void Fire()
    {
        InvokeRepeating("FireShot", 0f, 0f);
    }

    private void FireShot()
    {
        _ammoFireElapsedTime += Time.deltaTime;

        if (_ammoFireElapsedTime > ammoFireDelay)
        {
            _ammoFireElapsedTime = 0;

            Instantiate(ammoPrefab, ammoSpawner.position, transform.rotation);
        }
    }

    private IEnumerator DelayFire()
    {
        float delayToFireStart = 10f;

        yield return new WaitForSecondsRealtime(delayToFireStart);

        Fire();
    }
}