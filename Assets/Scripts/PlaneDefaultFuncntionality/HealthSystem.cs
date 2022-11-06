using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health fields")]
    [Space(10)]

    [Min(1)] public int health;

    public bool isDeath;

    public TMP_Text hpText;

    [Header("Materials")]
    [Space(10)]
    [SerializeField] private Material damagedMaterial;

    private CollisionSystem _collisionSystem;

    private ShootingSystem _shootingSystem;

    private MeshRenderer _bodyMeshRenderer;

    private void Start()
    {
        InitializeFields();
    }

    private void Update()
    {
        CheckState();

        if (hpText != null) hpText.text = health.ToString();
    }

    private void InitializeFields()
    {
        _collisionSystem = GetComponent<CollisionSystem>();

        _shootingSystem = GetComponent<ShootingSystem>();

        _bodyMeshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void CheckState()
    {
        if (health <= 2)
        {
            _bodyMeshRenderer.material = damagedMaterial;

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