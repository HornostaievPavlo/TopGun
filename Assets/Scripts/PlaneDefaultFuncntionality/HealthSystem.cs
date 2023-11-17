using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;

    private int _currentHealth;

    //[HideInInspector] 
    public bool isDead;

    public event Action<float> OnHealthPercentChanged = delegate { };

    private CollisionSystem _collisionSystem;

    private ShootingSystem _shootingSystem;

    public HealthBar healthBar;

    private void OnEnable()
    {
        _currentHealth = maxHealth;
    }

    private void Start()
    {
        InitializeFields();

        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.gameObject.SetActive(false);
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
        healthBar.gameObject.SetActive(true);

        _currentHealth -= amount;

        float currentHealthPercent = (float)_currentHealth / (float)maxHealth;

        OnHealthPercentChanged(currentHealthPercent);

        StartCoroutine(HideHealthBar());
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

            isDead = true;

            _collisionSystem.enabled = false;

            _shootingSystem.enabled = false;
        }
    }

    private IEnumerator HideHealthBar()
    {
        float delay = 3f;
        yield return new WaitForSecondsRealtime(delay);

        healthBar.gameObject.SetActive(false);
    }
}