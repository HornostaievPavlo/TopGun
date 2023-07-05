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

    [SerializeField] private Rigidbody torqueRigidbody;

    private float _torquePower = 1f;

    private Vector3 _torqueDirection;

    private float _dodgeTimer;

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private PlayerShootingSystem _shootingSystem;

    private BoxCollider _parentCollider;

    private void Start()
    {
        InitializeFields();
    }

    private void Update()
    {
        Move();
    }

    private void InitializeFields()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        _parentCollider = GetComponent<BoxCollider>();

        _torqueDirection = new Vector3(-1, 0, 0);

        _dodgeTimer = 0f;

        StartCoroutine(GetShootingSystem());
    }

    private void Move()
    {
        float verticalScreenBorder = 4.0f;
        float horizontalScreenBorder = 8.0f;

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (!_healthSystem.isDead)
        {
            transform.Translate(direction * Time.deltaTime * horizontalMovementSpeed);

            if (transform.position.y > verticalScreenBorder)
            {
                transform.position = new Vector3(transform.position.x, verticalScreenBorder, transform.position.z);
            }
            if (transform.position.y < -verticalScreenBorder)
            {
                transform.position = new Vector3(transform.position.x, -verticalScreenBorder, transform.position.z);
            }
            if (transform.position.x > horizontalScreenBorder)
            {
                transform.position = new Vector3(horizontalScreenBorder, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -horizontalScreenBorder)
            {
                transform.position = new Vector3(-horizontalScreenBorder, transform.position.y, transform.position.z);
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
            PerformDodge();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DisableDodge();
        }
    }

    private void PerformDodge()
    {
        float dodgeTimeScale = 0.5f;

        _gameManager.SetTimeScale(dodgeTimeScale);

        torqueRigidbody.AddRelativeTorque(_torqueDirection * _torquePower, ForceMode.VelocityChange);

        _parentCollider.enabled = false;

        _shootingSystem.enabled = false;

        _dodgeTimer += (Time.deltaTime * Time.timeScale);
    }

    private void DisableDodge()
    {
        float dodgeStateChangeTime = 1f;

        float timeScaleResetDelay = 1f;

        float slowingTimer = 0.1f;

        StartCoroutine(_gameManager.ResetTimeScale(timeScaleResetDelay));

        _shootingSystem.enabled = true;

        _parentCollider.enabled = true;

        if (_dodgeTimer >= dodgeStateChangeTime)
        {
            StartCoroutine(SlowTorque(slowingTimer));
        }
        if (_dodgeTimer < dodgeStateChangeTime)
        {
            float torqueStopDelay = 0.5f;
            StartCoroutine(StopTorque(torqueStopDelay));
        }

        _dodgeTimer = 0;
    }

    private IEnumerator SlowTorque(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        float stopTimer = 3f;

        float slowingMultiplier = 150f;

        torqueRigidbody.AddTorque(_torqueDirection * (-_torquePower * slowingMultiplier));

        StartCoroutine(StopTorque(stopTimer));
    }

    private IEnumerator StopTorque(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Vector3 currentRotation = new Vector3(torqueRigidbody.transform.eulerAngles.x,
                                               torqueRigidbody.transform.eulerAngles.y,
                                               torqueRigidbody.transform.eulerAngles.z);

        torqueRigidbody.transform.eulerAngles = currentRotation;

        // stops rigidbody from endless torque 
        torqueRigidbody.isKinematic = true;
        torqueRigidbody.isKinematic = false;
    }

    /// <summary>
    /// Waits one frame to let ShootingSystem component
    /// assign PlayerShootingSystem component
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetShootingSystem()
    {
        yield return new WaitForEndOfFrame();

        _shootingSystem = GetComponent<PlayerShootingSystem>();
    }
}
