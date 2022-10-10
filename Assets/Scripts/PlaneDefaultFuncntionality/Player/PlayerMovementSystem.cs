using System.Collections;
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

    public float slowingTimer;
    public float resetTimer;

    public float slowingMultiplier;

    public float timer = 0f;

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

            Dodge();
        }
        else
        {
            _collisionSystem.FallDown();
        }
    }

    private void Dodge()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddTorque(torqueDir * torquePower);

            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (timer >= 3)
            {
                Debug.LogError("baza");
                StartCoroutine(SlowTorque());
            }
            if (timer < 3)
            {
                Debug.LogWarning("too short");
                StartCoroutine(StopTorque(1f));
            }

            timer = 0;

        }
    }
    private IEnumerator SlowTorque()
    {
        yield return new WaitForSecondsRealtime(slowingTimer);

        rb.AddTorque(torqueDir * (-torquePower * slowingMultiplier));

        StartCoroutine(StopTorque(resetTimer));
    }

    private IEnumerator StopTorque(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Vector3 _currentRotation = new Vector3(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y, rb.transform.eulerAngles.z);

        rb.transform.eulerAngles = _currentRotation;

        rb.isKinematic = true;
        rb.isKinematic = false;
    }
}