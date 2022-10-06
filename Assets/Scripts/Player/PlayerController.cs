using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _ammoSpawner;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private GameObject _bombPrefab;

    [SerializeField]
    [Range(0, 10)] private float _horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float _verticalMovementSpeed;

    private float _bulletFireElapsedTime;
    private float _bombFireElapsedTime;

    private void Start()
    {
        _bulletFireElapsedTime = 0f;
        _bombFireElapsedTime = 0f;
    }

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
            Input.GetKey(KeyCode.D)) transform.Translate(direction * Time.deltaTime * _horizontalMovementSpeed);

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

        _bulletFireElapsedTime += Time.deltaTime;
        _bombFireElapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        //&& _bombFireElapsedTime > bombFireDelay)
        {
            _bombFireElapsedTime = 0;

            Instantiate(_bombPrefab, _ammoSpawner.position, transform.rotation);
        }
        if (Input.GetKey(KeyCode.Mouse0) && _bulletFireElapsedTime > bulletFireDelay)
        {
            _bulletFireElapsedTime = 0;

            Instantiate(_bulletPrefab, _ammoSpawner.position, transform.rotation);
        }
    }
}