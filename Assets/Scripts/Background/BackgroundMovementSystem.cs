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
}