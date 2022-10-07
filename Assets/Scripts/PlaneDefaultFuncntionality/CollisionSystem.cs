using UnityEngine;
using System.Collections;

public class CollisionSystem : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private HealthSystem _healthSystem;

    [Range(0, 1)] public float _explosionTimeScaleVar;

    private Collider[] _childColliders;

    private Rigidbody _parentRigidbody;



    private float _explosionForce;

    private void Start()
    {
        _explosionForce = Random.Range(500, 5000);

        _parentRigidbody = GetComponentInChildren<Rigidbody>();

        _childColliders = GetComponentsInChildren<Collider>();

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
        //Debug.LogWarning("Bomb hit");

        ExplodeEnemy();

        _gameManager.SetTimeScale(_explosionTimeScaleVar);

        StartCoroutine(_gameManager.ResetTimeScale(2));
    }

    public void FallDown()
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
        _healthSystem._isDeath = true;

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

        _healthSystem._explosionParticleSystem.SetActive(true);

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