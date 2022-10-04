using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // try the hor movement added to enemy for better interactability
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float verticalMovementSpeed;

    //Explosion 
    public float radius = 0.5f;
    public float force = 750;

    //rigidbody setup after explosion
    public float rbMass = 1;
    public float rbDrag = 1;

    ///////////////

    [SerializeField] private bool isDeath;

    [SerializeField] private Rigidbody parentRigidbody;

    [SerializeField] private MeshRenderer _bodyMeshRenderer;

    [SerializeField] private Material damagedMaterial;

    private Collider[] childColliders;

    private bool isMovingUp;

    public int health;

    private void Start()
    {
        isDeath = false;

        childColliders = GetComponentsInChildren<Collider>();
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
            DestroyWithBullet();
        }

        if (transform.position.y < Screen.height / -154) Destroy(gameObject);
    }

    private void CalculateHealth()
    {
        if (health <= 0) isDeath = true;

        if (health > 0 && health <= 2) _bodyMeshRenderer.material = damagedMaterial;
    }

    /// <summary>
    /// Moves enemy down after bullet hit depending on lives 
    /// </summary>
    public void DestroyWithBullet()
    {
        float fallSpeed = 1.5f;

        Vector3 fallDirection = new Vector3(0.5f, -1, 0);

        Vector3 torqueDirection = new Vector3(7.5f, 0, 5);

        transform.Translate(fallDirection * Time.deltaTime * fallSpeed);

        parentRigidbody.AddRelativeTorque(torqueDirection);

        StartCoroutine(DestroyColliders());
    }

    // just explosion from single bomb
    public void DestroyWithBomb()
    {
        Vector3 forceStartPos = transform.position;

        foreach (Collider collider in childColliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.mass = rbMass;

                rb.drag = rbDrag;

                rb.isKinematic = false;

                rb.AddExplosionForce(force, forceStartPos, radius);

                rb.useGravity = true;
            }
        }

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