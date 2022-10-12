using UnityEngine;

public class EnemyMovementSystem : MonoBehaviour
{
    [SerializeField] private GameObject _movePointsParent;

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    // moving through points array

    public Transform[] _movePoints;

    public float _moveSpeed;

    public int _currentIndex;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        _healthSystem = GetComponent<HealthSystem>();

        _collisionSystem = GetComponent<CollisionSystem>();

        _currentIndex = 1;

        _movePoints = _movePointsParent.GetComponentsInChildren<Transform>();

        transform.position = _movePoints[_currentIndex].position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!_healthSystem._isDeath)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                        _movePoints[_currentIndex].position,
                                        _moveSpeed * Time.deltaTime);

            if (transform.position == _movePoints[_currentIndex].position) _currentIndex++;

            if (_currentIndex == _movePoints.Length) _currentIndex = 1;

        }
        else
        {
            _collisionSystem.FallDown();
        }
    }

    // left for testing
    //[SerializeField]
    //[Range(0, 10)] private float _verticalMovementSpeed;

    //private bool _isMovingUp;

    //private void UpSideDownMovement()
    //{
    //    if (!_healthSystem._isDeath)
    //    {
    //        int movingRestrictionPoint = Screen.height / 270;

    //        if (transform.position.y > movingRestrictionPoint)
    //        {
    //            _isMovingUp = false;
    //        }
    //        if (transform.position.y < -movingRestrictionPoint)
    //        {
    //            _isMovingUp = true;
    //        }

    //        if (!_isMovingUp)
    //        {
    //            transform.Translate(Vector3.down * Time.deltaTime * _verticalMovementSpeed);
    //        }
    //        else
    //        {
    //            transform.Translate(Vector3.up * Time.deltaTime * _verticalMovementSpeed);
    //        }
    //    }
    //    else
    //    {
    //        _collisionSystem.FallDown();
    //    }
    //}
}