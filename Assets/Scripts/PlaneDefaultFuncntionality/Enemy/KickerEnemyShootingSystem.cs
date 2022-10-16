using UnityEngine;

[RequireComponent(typeof(ShootingSystem))]
public class KickerEnemyShootingSystem : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private Transform _player;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!_healthSystem._isDeath)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                        _player.position,
                                        2 * Time.deltaTime);
        }
    }

    private void FallDown()
    {

    }
}