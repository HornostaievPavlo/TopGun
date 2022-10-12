using UnityEngine;

public class BackgroundMovementSystem : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private SpriteRenderer _renderer;

    private Vector3 _startPosition;

    private float _width;

    private void Start()
    {
        _startPosition = transform.position;

        _renderer = GetComponentInChildren<SpriteRenderer>();

        _width = _renderer.bounds.size.x;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        if (transform.position.x < _startPosition.x - _width)
        {
            transform.position = _startPosition;
        }
    }

    //[SerializeField] private float _movementSpeed = 1f;

    //private float _offset;

    //private Vector3 _startPosition;

    //private float _newPosition;

    //private void Start()
    //{
    //    _startPosition = transform.position;

    //    _offset = 25.86f;
    //}

    //private void Update()
    //{
    //    _newPosition = Mathf.Repeat(Time.time * _movementSpeed, _offset);

    //    transform.position = _startPosition + Vector3.left * _newPosition;
    //}
}