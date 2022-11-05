using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSystem : MonoBehaviour
{
    // all from scratch

    [Range(0, 1)] public float _explosionTimeScaleValue;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private GameObject _explosionParticleSystem;

    public GameObject _fireParticleSystem;

    private HealthSystem _healthSystem;

    private ShootingSystem _shootingSystem;

    //

    public float _explosionForce;

    public float _explosionRadius;

    /// <summary>
    /// starting normal implementation
    /// </summary>    

    //public GameObject meshesParent;    

    public BoxCollider parentCollider;

    public Rigidbody parentRigidbody;

    public List<MeshCollider> meshColliders;

    public Transform[] childMeshes;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _shootingSystem = GetComponent<ShootingSystem>();

        //

        parentCollider = GetComponent<BoxCollider>();

        parentRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) // collision between planes
    {
        bool isGameEntityAttached = collision.gameObject.TryGetComponent<GameEntity>(out GameEntity gameEntity);

        if (isGameEntityAttached)
        {
            if (gameEntity._type == PlaneType.Player)
            {
                Debug.Log("udar");

                ApplyBombDamage(gameObject);
                ApplyBombDamage(collision.gameObject);
            }
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

    private void ApplyBulletDamage()
    {
        Debug.Log("bullet");

        _healthSystem._health -= 1;
    }

    private void ApplyBombDamage(GameObject objectToExplode)
    {
        Debug.Log("bomb");

        Explode(objectToExplode);

        _gameManager.SetTimeScale(_explosionTimeScaleValue);

        StartCoroutine(_gameManager.ResetTimeScale(2));
    }

    private void Explode(GameObject target)
    {
        HealthSystem healthSystem = target.GetComponent<HealthSystem>();
        ShootingSystem shootingSystem = target.GetComponent<ShootingSystem>();
        //CollisionSystem _collisionSystem = _objectToExplode.GetComponent<CollisionSystem>();

        healthSystem._isDeath = true;

        shootingSystem.enabled = false;

        //_collisionSystem.enabled = false;

        /// <summary>
        /// starting normal implementation
        /// </summary>   

        Destroy(parentCollider);

        Destroy(parentRigidbody);

        for (int i = 0; i < childMeshes.Length; i++)
        {
            var collider = childMeshes[i].gameObject.AddComponent<MeshCollider>();

            collider.convex = true;

            meshColliders.Add(childMeshes[i].GetComponent<MeshCollider>());
        }

        //        

        Vector3 forceStartPos = target.transform.position;

        foreach (Collider collider in meshColliders)
        {
            Rigidbody rb = collider.gameObject.AddComponent<Rigidbody>();

            rb.AddExplosionForce(_explosionForce, forceStartPos, _explosionRadius);

            rb.drag = 2f;

            rb.useGravity = true;

            parentRigidbody.useGravity = true;
        }

        _explosionParticleSystem.SetActive(true);

        Destroy(_fireParticleSystem.gameObject);

        StartCoroutine(DestroyColliders());
    }

    public void FallDown()
    {
        float fallSpeed = 1.5f;

        Vector3 fallDirection = new Vector3(0.5f, -1, 0);

        Vector3 torqueDirection = new Vector3(7.5f, 0, 5);

        transform.Translate(fallDirection * Time.deltaTime * fallSpeed);

        parentRigidbody.AddTorque(torqueDirection);

        StartCoroutine(DestroyColliders());

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyColliders()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        foreach (Collider item in meshColliders)
        {
            //if (item != null)
            //{
            Destroy(item.gameObject.GetComponent<Collider>());
            //}
        }
    }
}