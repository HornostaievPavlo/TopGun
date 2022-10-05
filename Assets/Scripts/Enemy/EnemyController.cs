using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // try the hor movement added to enemy for better interactability
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float verticalMovementSpeed;    

    [SerializeField]
    private bool isDeath;

    [SerializeField] private Rigidbody parentRigidbody;

    [SerializeField] private MeshRenderer _bodyMeshRenderer;

    [SerializeField] private Material damagedMaterial;

    [SerializeField] private GameObject explosionParticle;

    [SerializeField] private GameObject fireParticle;    

    private Collider[] childColliders;

    private bool isMovingUp;

    public int health; 

    private float radius = 0.75f;

    private float explosionForce;

    private float rbMass = 1;
    private float rbDrag = 1;

    private void Start()
    {
        isDeath = false;

        childColliders = GetComponentsInChildren<Collider>();

        explosionForce = Random.Range(500, 5000);
    }

    private void Update()
    {
        Move();

        CalculateHealth();
    }

    private void Move()
    {
        if (!isDeath)
        {
            int movingRestrictionPoint = Screen.height / 270;

            if (transform.position.y > movingRestrictionPoint)
            {
                isMovingUp = false;
            }
            if (transform.position.y < -movingRestrictionPoint)
            {
                isMovingUp = true;
            }

            if (!isMovingUp)
            {
                transform.Translate(Vector3.down * Time.deltaTime * verticalMovementSpeed);
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * verticalMovementSpeed);
            }
        }
        else
        {
            FallDown();
        }
    }

    private void CalculateHealth()
    {
        if (health == 0)
        {
            isDeath = true;
        }

        if (health <= 2)
        {
            _bodyMeshRenderer.material = damagedMaterial;

            fireParticle.SetActive(true);
        }
    }

    private void FallDown()
    {
        float fallSpeed = 1.5f;

        Vector3 fallDirection = new Vector3(0.5f, -1, 0);

        Vector3 torqueDirection = new Vector3(7.5f, 0, 5);

        transform.Translate(fallDirection * Time.deltaTime * fallSpeed);

        parentRigidbody.AddRelativeTorque(torqueDirection);

        StartCoroutine(DestroyColliders());

        if (transform.position.y < -10)
        {
            Debug.LogWarning("Out of the game");

            Destroy(gameObject);
        }
    }

    // just explosion from single bomb
    public void DestroyWithBomb()
    {
        isDeath = true;

        Vector3 forceStartPos = transform.position;

        foreach (Collider collider in childColliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.mass = rbMass;

                rb.drag = rbDrag;

                rb.isKinematic = false;

                rb.AddExplosionForce(explosionForce, forceStartPos, radius);

                rb.useGravity = true;
            }
        }

        explosionParticle.SetActive(true);

        StartCoroutine(DestroyColliders());
    }

    private IEnumerator DestroyColliders()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        foreach (Collider item in childColliders)
        {
            if (item != null)
            {
                Destroy(item.gameObject.GetComponent<Collider>());
            }
        }
    }
}