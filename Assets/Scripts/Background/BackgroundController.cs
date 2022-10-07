using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    public Vector3 startPosition;

    public float width;

    public SpriteRenderer _renderer;

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