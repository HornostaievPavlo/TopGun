using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Base class for enemy entity, implements movement and destroy methods 


    // try the hor movement added to enemy for better interactability
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float _verticalMovementSpeed;

    [SerializeField] private Material _damagedMaterial;

    [SerializeField] private GameObject _explosionParticleSystem;

    [SerializeField] private GameObject _fireParticleSystem;

    public int _health;

    private Rigidbody _parentRigidbody;

    private MeshRenderer _bodyMeshRenderer;

    private Collider[] _childColliders;

    private bool _isDeath;

    private bool _isMovingUp;

    private float _explosionForce;

    private void Start()
    {
        _isDeath = false;

        _parentRigidbody = GetComponentInChildren<Rigidbody>();

        _bodyMeshRenderer = GetComponentInChildren<MeshRenderer>();

        _childColliders = GetComponentsInChildren<Collider>();

        _explosionForce = Random.Range(500, 5000);
    }

    private void Update()
    {
        Move();

        CalculateHealth();
    }

    private void Move()
    {
        if (!_isDeath)
        {
            int movingRestrictionPoint = Screen.height / 270;

            if (transform.position.y > movingRestrictionPoint)
            {
                _isMovingUp = false;
            }
            if (transform.position.y < -movingRestrictionPoint)
            {
                _isMovingUp = true;
            }

            if (!_isMovingUp)
            {
                transform.Translate(Vector3.down * Time.deltaTime * _verticalMovementSpeed);
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * _verticalMovementSpeed);
            }
        }
        else
        {
            FallDown();
        }
    }

    private void CalculateHealth()
    {
        if (_health == 0)
        {
            _isDeath = true;
        }

        if (_health <= 2)
        {
            _bodyMeshRenderer.material = _damagedMaterial;

            _fireParticleSystem.SetActive(true);
        }
    }

    private void FallDown()
    {
        float fallSpeed = 1.5f;

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

    // just explosion from single bomb
    public void ExplodeEnemy()
    {
        _isDeath = true;

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

        StartCoroutine(DestroyColliders());
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