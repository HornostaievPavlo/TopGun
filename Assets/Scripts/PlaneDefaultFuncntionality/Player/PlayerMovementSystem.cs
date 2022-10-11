using System.Collections;
using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("3 should be ok for game ready")]
    [Range(0, 10)] private float _horizontalMovementSpeed;

    [SerializeField]
    [Tooltip("3 should be ok for game ready")]
    [Range(0, 10)] private float _verticalMovementSpeed;

    [SerializeField] private GameManager _gameManager;

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private ShootingSystem _shootingSystem;

    //

    private Rigidbody _rigidbody;
    private Vector3 _torqueDirection;

    [SerializeField] private float _torquePower; // 1

    [Tooltip("Time until slowing begins")]
    [SerializeField] private float slowingTimer; // 0.1f

    [Tooltip("Time until stopping begins")]
    [SerializeField] private float stopTimer; // 3

    [Tooltip("Multiplier for opposite torque")]
    [SerializeField] private float _slowingMultiplier; // 150

    public float _torqueTimer;

    private BoxCollider _collider;

    //public float timeScale; // test

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        _shootingSystem = GetComponent<ShootingSystem>();

        _rigidbody = GetComponentInChildren<Rigidbody>();

        _collider = GetComponent<BoxCollider>();

        _torqueDirection = new Vector3(-1, 0, 0);

        _torqueTimer = 0f;
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
            EnableDodge();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DisableDodge();
        }
    }

    private void EnableDodge()
    {
        float _dodgeTimeScale = 0.75f;

        _rigidbody.AddTorque(_torqueDirection * _torquePower);

        _shootingSystem.enabled = false;

        _collider.enabled = false;

        _gameManager.SetTimeScale(_dodgeTimeScale);

        _torqueTimer += (Time.deltaTime * Time.timeScale);
    }

    private void DisableDodge()
    {
        float _dodgeStateChangingValue = 1f;

        float _timeScaleResetDelay = 2f;

        StartCoroutine(_gameManager.ResetTimeScale(_timeScaleResetDelay));

        _shootingSystem.enabled = true;

        _collider.enabled = true;

        if (_torqueTimer >= _dodgeStateChangingValue)
        {
            Debug.LogError("baza");
            StartCoroutine(SlowTorque(slowingTimer));
        }
        if (_torqueTimer < _dodgeStateChangingValue)
        {
            float _torqueStopDelay = 0.5f;
            Debug.LogWarning("too short");
            StartCoroutine(StopTorque(_torqueStopDelay));
        }

        _torqueTimer = 0;
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