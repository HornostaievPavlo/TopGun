using UnityEngine;

public class EnemyMovementSystem : MonoBehaviour
{
    [SerializeField] public GameObject _movePointsParent;

    [SerializeField] private float moveSpeed;

    private HealthSystem _healthSystem;

    private CollisionSystem _collisionSystem;

    private Transform[] _movePoints;

    private int _currentIndex;

    private void Start()
    {
        InitializeFields();
    }

    private void InitializeFields()
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
        if (!_healthSystem.isDeath)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                         _movePoints[_currentIndex].position,
                                         moveSpeed * Time.deltaTime);

            if (transform.position == _movePoints[_currentIndex].position) _currentIndex++;

            if (_currentIndex == _movePoints.Length) _currentIndex = 1;

        }
        else
        {
            _collisionSystem.FallDown();
        }
    }
}