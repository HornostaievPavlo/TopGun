using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSystem : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Explosion")]
    [Space(10)]

    [Tooltip("All children of object to be exploded")]
    public Transform[] explosionObjects;

    [Range(0, 1)] public float explosionTimeScaleValue = 0.1f;

    [SerializeField] private GameObject explosionParticleSystem;

    public float _explosionForce; // make private on ready state

    public float _explosionRadius; // make private on ready state

    private BoxCollider _parentCollider;

    private Rigidbody _parentRigidbody;

    private List<MeshCollider> _explosionColliders = new List<MeshCollider>();

    [Header("Bullet Hit")]
    [Space(10)]

    [Tooltip("Speed with which killed plane falls down")]
    [SerializeField] private float fallingSpeed; // make private on ready state

    public Rigidbody torqueRigidbody;

    public GameObject fireParticleSystem;

    private HealthSystem _healthSystem;

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
            switch (ammoController._type)
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
    }

    private void ApplyBulletDamage()
    {
        _healthSystem.health -= 1;
    }

    private void ApplyBombDamage(GameObject objectToExplode)
    {
        Explode(objectToExplode);

        gameManager.SetTimeScale(explosionTimeScaleValue);

        StartCoroutine(gameManager.ResetTimeScale(2));
    }

    private void Explode(GameObject target)
    {
        HealthSystem healthSystem = target.GetComponent<HealthSystem>();
        ShootingSystem shootingSystem = target.GetComponent<ShootingSystem>();
        CollisionSystem collisionSystem = target.GetComponent<CollisionSystem>();

        healthSystem.isDeath = true;
        shootingSystem.enabled = false;
        collisionSystem.enabled = false;

        Destroy(_parentCollider);

        Destroy(_parentRigidbody);

        for (int i = 0; i < explosionObjects.Length; i++)
        {
            var collider = explosionObjects[i].gameObject.AddComponent<MeshCollider>();

            collider.convex = true;

            _explosionColliders.Add(explosionObjects[i].GetComponent<MeshCollider>());
        }

        Vector3 forceStartPos = target.transform.position;

        foreach (Collider collider in _explosionColliders)
        {
            Rigidbody rb = collider.gameObject.AddComponent<Rigidbody>();

            rb.AddExplosionForce(_explosionForce, forceStartPos, _explosionRadius);

            rb.drag = 2f;

            rb.useGravity = true;

            _parentRigidbody.useGravity = true;
        }

        explosionParticleSystem.SetActive(true);

        Destroy(fireParticleSystem.gameObject);

        StartCoroutine(DestroyColliders());
    }

    public void FallDown()
    {
        float fallSpeed = 1.5f;

        Vector3 fallDirection = new Vector3(0.5f, -1, 0);

        Vector3 torqueDirection = new Vector3(7.5f, 0, 5);

        transform.Translate(fallDirection * Time.deltaTime * fallSpeed);

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