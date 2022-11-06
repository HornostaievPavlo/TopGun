using TMPro;
using UnityEngine;

//[RequireComponent(typeof(CollisionSystem))]
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Material _damagedMaterial;

    private CollisionSystem _collisionSystem;

    private ShootingSystem _shootingSystem;

    private MeshRenderer _bodyMeshRenderer;

    public int _health;

    public bool _isDeath;

    [SerializeField] private TMP_Text hpText;

    void Start()
    {
        InitializeFields();
    }

    void Update()
    {
        UpdateState();

        if (hpText != null) hpText.text = _health.ToString();
    }

    private void InitializeFields()
    {
        _collisionSystem = GetComponent<CollisionSystem>();

        _shootingSystem = GetComponent<ShootingSystem>();

        _bodyMeshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void UpdateState()
    {
        if (_health == 0)
        {
            _isDeath = true;

            _collisionSystem.enabled = false;

            _shootingSystem.enabled = false;
        }

        if (_health <= 2)
        {
            _bodyMeshRenderer.material = _damagedMaterial;

            if (_collisionSystem.fireParticleSystem != null)
                _collisionSystem.fireParticleSystem.SetActive(true);
        }
    }
}