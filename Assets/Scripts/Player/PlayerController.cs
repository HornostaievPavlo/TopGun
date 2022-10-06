using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform ammoSpawner;

    [SerializeField]
    [Range(0, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float verticalMovementSpeed;

    private float bulletFireElapsedTime = 0f;
    private float bombFireElapsedTime = 0f;

    private void Update()
    {
        Move();

        Fire();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        
        float verticalBorder = 4.0f;
        float horizontalBorder = 8.0f;

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)) transform.Translate(direction * Time.deltaTime * horizontalMovementSpeed);

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
        //float bombFireDelay = 3f;
        float bulletFireDelay = 0.15f;

        bulletFireElapsedTime += Time.deltaTime;
        bombFireElapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        //&& bombFireElapsedTime > bombFireDelay)
        {
            //bombFireElapsedTime = 0;

            Instantiate(bombPrefab, ammoSpawner.position, transform.rotation);
        }
        if (Input.GetKey(KeyCode.Mouse0) && bulletFireElapsedTime > bulletFireDelay)
        {
            bulletFireElapsedTime = 0;

            Instantiate(bulletPrefab, ammoSpawner.position, transform.rotation);
        }
    }
}