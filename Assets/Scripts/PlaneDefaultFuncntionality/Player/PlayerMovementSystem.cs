using System.Collections;
using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [Header("Movement")]
    [Space(10)]

    [Range(0, 10)]
    [SerializeField] private float horizontalMovementSpeed; // 3 should be ok for game ready

    [Range(0, 10)]
    [SerializeField] private float verticalMovementSpeed; // 3 should be ok for game ready

    [Header("Dodge feature")]
    [Space(10)]

    [SerializeField] private float torquePower = 1f; // 1

    [Tooltip("Time until slowing begins")]
    [SerializeField] private float slowingTimer = 0.1f; // 0.1f

    [Tooltip("Time until stopping begins")]
    [SerializeField] private float stopTimer = 3f; // 3

    [Tooltip("Multiplier for opposite torque")]
    [SerializeField] private float slowingMultiplier = 150f; // 150

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private PlayerShootingSystem _shootingSystem;

    public Rigidbody torqueRigidbody;
    private BoxCollider _collider;

    private Vector3 _torqueDirection;
    private float _dodgeTimer;

    private void Start()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        _collider = GetComponent<BoxCollider>();

        _torqueDirection = new Vector3(-1, 0, 0);

        _dodgeTimer = 0f;

        StartCoroutine(GetShootingSystem());
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
            transform.Translate(direction * Time.deltaTime * horizontalMovementSpeed);

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
        float dodgeTimeScale = 0.5f;

        _gameManager.SetTimeScale(dodgeTimeScale);

        torqueRigidbody.AddRelativeTorque(_torqueDirection * torquePower, ForceMode.VelocityChange);

        _shootingSystem.enabled = false;

        _collider.enabled = false;

        _dodgeTimer += (Time.deltaTime * Time.timeScale);
    }

    private void DisableDodge()
    {
        float _dodgeStateChangeTime = 1f;

        float _timeScaleResetDelay = 2f;

        StartCoroutine(_gameManager.ResetTimeScale(_timeScaleResetDelay));

        _shootingSystem.enabled = true;

        _collider.enabled = true;

        if (_dodgeTimer >= _dodgeStateChangeTime)
        {
            StartCoroutine(SlowTorque(slowingTimer));
        }
        if (_dodgeTimer < _dodgeStateChangeTime)
        {
            float _torqueStopDelay = 0.5f;
            StartCoroutine(StopTorque(_torqueStopDelay));
        }

        _dodgeTimer = 0;
    }

    private IEnumerator SlowTorque(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        torqueRigidbody.AddTorque(_torqueDirection * (-torquePower * slowingMultiplier));

        StartCoroutine(StopTorque(stopTimer));
    }

    private IEnumerator StopTorque(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Vector3 _currentRotation = new Vector3(torqueRigidbody.transform.eulerAngles.x, torqueRigidbody.transform.eulerAngles.y, torqueRigidbody.transform.eulerAngles.z);

        torqueRigidbody.transform.eulerAngles = _currentRotation;

        torqueRigidbody.isKinematic = true;
        torqueRigidbody.isKinematic = false;
    }

    private IEnumerator GetShootingSystem()
    {
        yield return new WaitForEndOfFrame();

        _shootingSystem = GetComponent<PlayerShootingSystem>();
    }
}
