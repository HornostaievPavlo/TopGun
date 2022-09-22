using System.Collections;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;
    //[SerializeField] private ParticleSystem playerFire;

    [SerializeField]
    [Range(0, 10)] private float fallRotationSpeed;

    private EnemyController _enemyController;

    private Rigidbody _rigidbody;

    public bool isDeath;

    private void Start()
    {
        isDeath = false;

        _enemyController = GetComponent<EnemyController>();

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("baza");
        Destroy(other.gameObject);

        isDeath = true;
    }

    private void UpdateState()
    {
        const float lowerBorder = -7;

        if (isDeath)
        {
            StartCoroutine(EnableGravity());

            transform.Rotate(Vector3.back * fallRotationSpeed);

            fire.Play();

            _enemyController.enabled = false;
        }

        if (transform.position.y < lowerBorder) Destroy(this.gameObject);
    }

    private IEnumerator EnableGravity()
    {
        yield return new WaitForSeconds(2);

        _rigidbody.useGravity = true;

        fallRotationSpeed = 0.5f;
    }
}