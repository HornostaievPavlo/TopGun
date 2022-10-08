using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)] private float _horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float _verticalMovementSpeed;

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        float verticalBorder = 4.0f;
        float horizontalBorder = 8.0f;

        if (!_healthSystem._isDeath)
        {
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
        else
        {
            _collisionSystem.FallDown();
        }
    }
}