using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSystem : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private ScoreManager scoreManager;

    [Header("Explosion")]
    [Space(10)]

    [Tooltip("All children of object to be exploded")]
    [SerializeField] private Transform[] explosionObjects;

    [SerializeField] private GameObject explosionParticleSystem;

    private BoxCollider _parentCollider;

    private Rigidbody _parentRigidbody;

    private List<MeshCollider> _explosionColliders = new List<MeshCollider>();

    private float _explosionForce;

    private float _explosionTimeScale = 0.1f;

    [Header("Bullet Hit")]
    [Space(10)]

    private const int BULLET_DAMAGE = 1;

    public Rigidbody torqueRigidbody;

    public GameObject fireParticleSystem;

    private HealthSystem _healthSystem;

    private float _fallingSpeed;

    private void Start()
    {
        InitializeFields();
    }

    private void OnCollisionEnter(Collision collision) // collision between planes
    {
        bool isGameEntityAttached = collision.gameObject.TryGetComponent<GameEntity>(out GameEntity gameEntity);

        if (isGameEntityAttached)
        {
            ApplyBombDamage(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) // trigger collision with bullet and bomb
    {
        bool isAmmoControllerAttached = other.gameObject.TryGetComponent<AmmoController>(out AmmoController ammoController);

        if (isAmmoControllerAttached)
        {
            switch (ammoController.type)
            {
                case AmmoType.Bullet:

                    ApplyBulletDamage();

                    break;

                case AmmoType.Bomb:

                    ApplyBombDamage(gameObject);

                    break;
            }
        }

        Destroy(other.gameObject);
    }

    private void InitializeFields()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _parentCollider = GetComponent<BoxCollider>();

        _parentRigidbody = GetComponent<Rigidbody>();

        _explosionForce = Random.Range(1000f, 5000f);

        _fallingSpeed = Random.Range(1f, 3f);
    }

    private void ApplyBulletDamage()
    {
        _healthSystem.ModifyHealth(BULLET_DAMAGE);
    }

    private void ApplyBombDamage(GameObject objectToExplode)
    {
        Explode(objectToExplode);

        gameManager.SetTimeScale(_explosionTimeScale);

        StartCoroutine(gameManager.ResetTimeScale(2));
    }

    private void Explode(GameObject target)
    {
        HealthSystem healthSystem = target.GetComponent<HealthSystem>();
        ShootingSystem shootingSystem = target.GetComponent<ShootingSystem>();
        CollisionSystem collisionSystem = target.GetComponent<CollisionSystem>();

        healthSystem.isDead = true;
        shootingSystem.enabled = false;
        collisionSystem.enabled = false;

        Destroy(_parentCollider);

        Destroy(_parentRigidbody);

        Destroy(GetComponentInChildren<HealthBar>().gameObject);

        for (int i = 0; i < explosionObjects.Length; i++)
        {
            var collider = explosionObjects[i].gameObject.AddComponent<MeshCollider>();

            collider.convex = true;

            _explosionColliders.Add(explosionObjects[i].GetComponent<MeshCollider>());
        }

        Vector3 forceStartPos = target.transform.position;

        float explosionRadius = 10f;

        foreach (Collider collider in _explosionColliders)
        {
            Rigidbody rb = collider.gameObject.AddComponent<Rigidbody>();

            rb.AddExplosionForce(_explosionForce, forceStartPos, explosionRadius);

            rb.drag = 1.5f;

            rb.useGravity = true;

            _parentRigidbody.useGravity = true;
        }

        explosionParticleSystem.SetActive(true);

        Destroy(fireParticleSystem.gameObject);

        StartCoroutine(DestroyColliders());
    }

    public void FallDown()
    {
        Vector3 fallDirection = new Vector3(0.5f, -1, 0);

        Vector3 torqueDirection = new Vector3(7.5f, 0, 5);

        transform.Translate(fallDirection * Time.deltaTime * _fallingSpeed);

        if (_parentRigidbody) torqueRigidbody.AddTorque(torqueDirection);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyColliders()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        foreach (Collider item in _explosionColliders)
        {
            Destroy(item);
        }
    }
}