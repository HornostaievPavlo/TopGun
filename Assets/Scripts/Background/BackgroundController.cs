using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Vector3 startPosition;

    private float halfWidth;

    private void Start()
    {
        startPosition = transform.position;

        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        if (transform.position.x < startPosition.x - halfWidth)
        {
            transform.position = startPosition;
        }
    }
}