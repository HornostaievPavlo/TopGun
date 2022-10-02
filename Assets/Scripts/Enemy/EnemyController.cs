using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // try the hor movement added to enemy for better interactability
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    // 
    [SerializeField]
    [Range(1, 10)] private float verticalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float bulletKilledFallForce;
    
    public int health;

    //Explosion 
    public float radius;
    public float force;

    private Collider[] colliders;
    //

    //rigidbody setup after explosion
    public float rbMass;
    public float rbDrag;
    //

    // state bool
    private bool isMovingUp;
    [SerializeField] private bool isDeath;
    //

    private void Start()
    {
        isDeath = false;

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
            Debug.LogError("death fucker");

            Destroy(gameObject.GetComponent<Collider>());

            DestroyWithBullet();
        }

        if (transform.position.y < Screen.height / -154) Destroy(gameObject);
    }

    private void CalculateHealth()
    {
        if (health <= 0) isDeath = true;
    }    

    // falls down after bullets hits
    // depends on lives calculation
    public void DestroyWithBullet()
    {
        transform.Translate(Vector3.down * Time.deltaTime * bulletKilledFallForce);

    }

    // just explosion from single bomb
    public void DestroyWithBomb()
    {
        Debug.Log("vzriv");

        Vector3 forceStartPos = transform.position;

        colliders = Physics.OverlapSphere(forceStartPos, radius);

        foreach (Collider collider in colliders)
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

    }
}