using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)] private float _horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float _verticalMovementSpeed;

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    //

    public Rigidbody rb;
    public Vector3 torqueDir;
    public float torquePower;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        rb = GetComponentInChildren<Rigidbody>();
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

            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Debug.Log("i pognal");
                rb.AddTorque(torqueDir * torquePower);
                Debug.Log(rb.transform.eulerAngles.x);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("stop");

                //Quaternion _currentRotation = new Quaternion
                //   (rb.transform.rotation.x, rb.transform.rotation.y, rb.transform.rotation.z, 0);

                rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y, rb.transform.eulerAngles.z);

                rb.isKinematic = true;
                rb.isKinematic = false;
            }
        }
        else
        {
            _collisionSystem.FallDown();
        }
    }
}