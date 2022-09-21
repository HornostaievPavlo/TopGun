using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform ammoSpawner;

    [SerializeField] private float horizontalMovementSpeed;
    [SerializeField] private float verticalMovementSpeed;

    private Transform propeller;

    private Vector3 moveDirection;

    private float fireElapsedTime = 0f;

    private void Start()
    {
        propeller = GetComponentInChildren<BoxCollider>().transform;
    }

    private void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        Move(moveDirection);

        CheckScreenBorders();

        fireElapsedTime += Time.deltaTime;

        Fire();

        RotatePropeller();
    }

    private void Move(Vector3 direction)
    {
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)) transform.Translate(direction * Time.deltaTime * horizontalMovementSpeed);

        //if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up * Time.deltaTime * horizontalMovementSpeed);
        //if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * verticalMovementSpeed);
        //if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down * Time.deltaTime * horizontalMovementSpeed);
        //if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * verticalMovementSpeed);
    }

    private void CheckScreenBorders()
    {
        const float verticalBorder = 4.0f;
        const float horizontalBorder = 8.0f;

        if (transform.position.y > verticalBorder)
        {
            transform.position = new Vector3(transform.position.x, verticalBorder, transform.position.z);
        }
        if (transform.position.y < -verticalBorder)
        {
            transform.position = new Vector3(transform.position.x, -verticalBorder, transform.position.z);
        }
        if (transform.position.x > horizontalBorder)
        {
            transform.position = new Vector3(horizontalBorder, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -horizontalBorder)
        {
            transform.position = new Vector3(-horizontalBorder, transform.position.y, transform.position.z);
        }
    }

    private void Fire()
    {
        const float bombFireDelay = 3f;
        const float bulletFireDelay = 0.15f;

        if (Input.GetKeyDown(KeyCode.Mouse1) && fireElapsedTime > bombFireDelay)
        {
            fireElapsedTime = 0;

            Instantiate(bombPrefab, ammoSpawner.position, transform.rotation);
        }
        if (Input.GetKey(KeyCode.Mouse0) && fireElapsedTime > bulletFireDelay)
        {
            fireElapsedTime = 0;

            Instantiate(bulletPrefab, ammoSpawner.position, transform.rotation);
        }
    }

    private void RotatePropeller()
    {
        propeller.Rotate(Vector3.left * 5);

        if (Input.GetKey("d"))
        {
            propeller.Rotate(Vector3.left * 7.5f);
        }
    }
}