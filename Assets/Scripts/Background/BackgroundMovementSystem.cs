using UnityEngine;

public class BackgroundMovementSystem : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private SpriteRenderer _renderer;

    private Vector3 startPosition;

    private float width;

    private void Start()
    {
        startPosition = transform.position;

        _renderer = GetComponentInChildren<SpriteRenderer>();

        width = _renderer.bounds.size.x;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        if (transform.position.x < startPosition.x - width)
        {
            transform.position = startPosition;
        }
    }
}