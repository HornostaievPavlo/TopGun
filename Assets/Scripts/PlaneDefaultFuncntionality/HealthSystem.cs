using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Min(1)] public int health;

    [HideInInspector] public bool isDeath;

    private CollisionSystem _collisionSystem;

    private ShootingSystem _shootingSystem;

    private void Start()
    {
        InitializeFields();
    }

    private void Update()
    {
        CheckState();
    }

    private void InitializeFields()
    {
        _collisionSystem = GetComponent<CollisionSystem>();

        _shootingSystem = GetComponent<ShootingSystem>();
    }

    private void CheckState()
    {
        if (health <= 2)
        {
            if (_collisionSystem.fireParticleSystem != null)
                _collisionSystem.fireParticleSystem.SetActive(true);
        }

        if (health == 0)
        {
            isDeath = true;

            _collisionSystem.enabled = false;

            _shootingSystem.enabled = false;
        }
    }
}