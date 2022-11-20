using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;

    private int _currentHealth;

    //[HideInInspector] 
    public bool isDeath;

    public event Action<float> OnHealthPercentChanged = delegate { };

    private CollisionSystem _collisionSystem;

    private ShootingSystem _shootingSystem;

    private void OnEnable()
    {
        _currentHealth = maxHealth;
    }

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

    public void ModifyHealth(int amount)
    {
        _currentHealth -= amount;

        float currentHealthPercent = (float)_currentHealth / (float)maxHealth;

        OnHealthPercentChanged(currentHealthPercent);
    }

    private void CheckState()
    {
        if (_currentHealth <= 2)
        {
            if (_collisionSystem.fireParticleSystem != null)
                _collisionSystem.fireParticleSystem.SetActive(true);
        }

        if (_currentHealth == 0)
        {
            HealthBar healthBar = GetComponentInChildren<HealthBar>();

            if (healthBar != null)
                Destroy(healthBar.gameObject);

            isDeath = true;

            _collisionSystem.enabled = false;

            _shootingSystem.enabled = false;
        }
    }
}