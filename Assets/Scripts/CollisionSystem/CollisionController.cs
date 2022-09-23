using System.Collections;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    //[SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;
    //[SerializeField] private ParticleSystem playerFire;

    //[SerializeField]
    //[Range(0, 10)] private float fallRotationSpeed;

    private EnemyController _enemyController;

    public bool isDeath;

    // explosion experiment

    public Transform[] children;

    private void Start()
    {
        isDeath = false;

        _enemyController = GetComponent<EnemyController>();

        // explosion experiment
        children = GetComponentsInChildren<Transform>();

        foreach (Transform item in children)
        {
            Rigidbody rb = item.gameObject.AddComponent<Rigidbody>();

            rb.useGravity = false;

            rb.drag = 4f;
        }
    }

    private void Update()
    {
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("baza");

        isDeath = true;

        Destroy(other.gameObject);
    }

    private void UpdateState()
    {
        if (isDeath)
        {
            _enemyController.enabled = false;

            // explosion experiment

            foreach (Transform item in children)
            {
                item.gameObject.AddComponent<BoxCollider>();
            }

            StartCoroutine(EnableGravity());

            isDeath = false;
        }

        float lowerBorder = -7;

        if (transform.position.y < lowerBorder)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator EnableGravity()
    {
        yield return new WaitForSeconds(1f);

        foreach (Transform item in children)
        {
            Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();

            rb.useGravity = true;

            rb.drag = 0;
        }
    }
}