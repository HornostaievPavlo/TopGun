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

    private Rigidbody _rigidbody;
    private Vector3 _torqueDirection;

    public float _torquePower;

    [Tooltip("Time until slowing begins")]
    public float slowingTimer;

    [Tooltip("Time until stopping begins")]
    public float stopTimer;

    [Tooltip("Multiplier for opposite torque")]
    public float _slowingMultiplier;

    public float _torqueTimer = 0f;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        _rigidbody = GetComponentInChildren<Rigidbody>();

        _torqueDirection = new Vector3(-1, 0, 0);
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
            _rigidbody.AddTorque(_torqueDirection * _torquePower);

            _torqueTimer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (_torqueTimer >= 2)
            {
                Debug.LogError("baza");
                StartCoroutine(SlowTorque(slowingTimer));
            }
            if (_torqueTimer < 2)
            {
                float _torqueStopDelay = 0.5f;
                Debug.LogWarning("too short");
                StartCoroutine(StopTorque(_torqueStopDelay));
            }

            _torqueTimer = 0;

        }
    }

    private IEnumerator SlowTorque(float delay) 
    {
        yield return new WaitForSecondsRealtime(delay);

        _rigidbody.AddTorque(_torqueDirection * (-_torquePower * _slowingMultiplier));

        StartCoroutine(StopTorque(stopTimer));
    }

    private IEnumerator StopTorque(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Vector3 _currentRotation = new Vector3(_rigidbody.transform.eulerAngles.x, _rigidbody.transform.eulerAngles.y, _rigidbody.transform.eulerAngles.z);

        _rigidbody.transform.eulerAngles = _currentRotation;

        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }
}