using System.Collections;
using UnityEngine;

public class CollisionSystem : MonoBehaviour
{
    // all from scratch

    [Range(0, 1)] public float _explosionTimeScaleValue;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private GameObject _explosionParticleSystem;

    private HealthSystem _healthSystem;

    private ShootingSystem _shootingSystem;


    public GameObject _fireParticleSystem;


    public Collider[] _childColliders;

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

        _childColliders = GetComponentsInChildren<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool _isAmmoControllerAttached = collision.gameObject.TryGetComponent<AmmoController>(out AmmoController _ammoController);

        if (_isAmmoControllerAttached)
        {
            switch (_ammoController._type)
            {
                case AmmoType.Bullet:
                    //Debug.Log("bullet");
                    ApplyBulletDamage();

                    break;

                case AmmoType.Bomb:
                    //Debug.Log("bomb");
                    ApplyBombDamage(gameObject);
                    break;
            }

            Destroy(collision.GetContact(0).otherCollider.gameObject);

        }

        bool _isGameEntityAttached = collision.gameObject.TryGetComponent<GameEntity>(out GameEntity _gameEntity);

        if (_isGameEntityAttached)
        {
            Debug.Log("baza");

            ApplyBombDamage(gameObject);
            ApplyBombDamage(collision.gameObject);
        }
    }

    private void ApplyBulletDamage()
    {
        _healthSystem._health -= 1;
    }

    private void ApplyBombDamage(GameObject _objectToExplode)
    {
        Explode(_objectToExplode);

        _gameManager.SetTimeScale(_explosionTimeScaleValue);

        StartCoroutine(_gameManager.ResetTimeScale(2));
    }

    private void Explode(GameObject _objectToExplode)
    {
        HealthSystem _healthSystem = _objectToExplode.GetComponent<HealthSystem>();
        ShootingSystem _shootingSystem = _objectToExplode.GetComponent<ShootingSystem>();
        //CollisionSystem _collisionSystem = _objectToExplode.GetComponent<CollisionSystem>();

        _healthSystem._isDeath = true;

        _shootingSystem.enabled = false;

        //_collisionSystem.enabled = false;

        float _explosionRadius = 1;

        Vector3 forceStartPos = _objectToExplode.transform.position;

        Collider[] a = _objectToExplode.GetComponent<CollisionSystem>()._childColliders;

        foreach (Collider collider in a)
        {
            if (collider.gameObject.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = collider.gameObject.AddComponent<Rigidbody>();

                rb.AddExplosionForce(_explosionForce, forceStartPos, _explosionRadius);

                rb.useGravity = true;

                GetComponent<Rigidbody>().useGravity = true;
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

        //_parentRigidbody.AddRelativeTorque(torqueDirection);

        StartCoroutine(DestroyColliders());

        if (transform.position.y < -10)
        {
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