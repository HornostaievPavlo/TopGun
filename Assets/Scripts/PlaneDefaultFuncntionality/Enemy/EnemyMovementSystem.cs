using UnityEngine;

public class EnemyMovementSystem : MonoBehaviour
{
    // try the hor movement added to enemy for better interactability
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(0, 10)] private float _verticalMovementSpeed;

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private bool _isMovingUp;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
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
        if (!_healthSystem._isDeath)
        {
            int movingRestrictionPoint = Screen.height / 270;

            if (transform.position.y > movingRestrictionPoint)
            {
                _isMovingUp = false;
            }
            if (transform.position.y < -movingRestrictionPoint)
            {
                _isMovingUp = true;
            }

            if (!_isMovingUp)
            {
                transform.Translate(Vector3.down * Time.deltaTime * _verticalMovementSpeed);
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * _verticalMovementSpeed);
            }
        }
        else
        {
            _collisionSystem.FallDown();
        }
    }
}