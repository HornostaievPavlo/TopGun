using System.Collections;
using UnityEngine;

public class CollisionSystem : MonoBehaviour
{
    [Range(0, 1)] public float _explosionTimeScaleValue;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private GameObject _explosionParticleSystem;

    private HealthSystem _healthSystem;

    private ShootingSystem _shootingSystem;

    private Rigidbody _parentRigidbody;

    public GameObject _fireParticleSystem;

    private Collider[] _childColliders;

    [SerializeField] private float _explosionForce;

    [SerializeField] private float fallingSpeed;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _shootingSystem = GetComponent<ShootingSystem>();

        _parentRigidbody = GetComponentInChildren<Rigidbody>();

        _childColliders = GetComponentsInChildren<Collider>();

        //_explosionForce = Random.Range(1, 1000);
    }

    private void OnTriggerEnter(Collider other)
    {
        AmmoType hitType = other.gameObject.GetComponent<AmmoController>().type;

        switch (hitType)
        {
            case AmmoType.Bullet:

                ApplyBulletDamage();
                break;

            case AmmoType.Bomb:

                ApplyBombDamage();
                break;
        }

        Destroy(other.gameObject);
    }

    private void ApplyBulletDamage()
    {
        //Debug.LogWarning("Bullet hit");

        _healthSystem._health -= 1;
    }

    private void ApplyBombDamage()
    {
        //Debug.LogError("Bomb hit");

        Explode();

        _gameManager.SetTimeScale(_explosionTimeScaleValue);

        StartCoroutine(_gameManager.ResetTimeScale(2));
    }

    private void Explode()
    {
        _healthSystem._isDeath = true;

        _shootingSystem.enabled = false;

        float _explosionRadius = 1;

        float _explodedRigidbodyMass = 1;
        float _explodedRigidbodyDrag = 1;

        Vector3 forceStartPos = transform.position;

        foreach (Collider collider in _childColliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.mass = _explodedRigidbodyMass;

                rb.drag = _explodedRigidbodyDrag;

                rb.isKinematic = false;

                rb.AddExplosionForce(_explosionForce, forceStartPos, _explosionRadius);

                rb.useGravity = true;
            }
        }

        _explosionParticleSystem.SetActive(true);

        Destroy(_fireParticleSystem.gameObject);

        StartCoroutine(DestroyColliders());
    }

    public void FallDown()
    {
        //float fallSpeed = 1.5f;
        float fallSpeed = fallingSpeed;

        Vector3 fallDirection = new Vector3(0.5f, -1, 0);

        Vector3 torqueDirection = new Vector3(7.5f, 0, 5);

        transform.Translate(fallDirection * Time.deltaTime * fallSpeed);

        _parentRigidbody.AddRelativeTorque(torqueDirection);

        StartCoroutine(DestroyColliders());

        if (transform.position.y < -10)
        {
            Debug.LogWarning("Out of the game");

            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyColliders()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        foreach (Collider item in _childColliders)
        {
            if (item != null)
            {
                Destroy(item.gameObject.GetComponent<Collider>());
            }
        }
    }
}