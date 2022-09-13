using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform ammoSpawner;

    [SerializeField] private float speed;

    private CharacterController characterController;

    private Vector3 moveDirection;

    private float fireElapsedTime = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        MovePlayer(moveDirection);

        FitScreenBorders();

        fireElapsedTime += Time.deltaTime;

        Fire();
    }

    private void MovePlayer(Vector3 direction)
    {
        characterController.Move(direction * speed * Time.deltaTime);
    }

    private void FitScreenBorders()
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
}