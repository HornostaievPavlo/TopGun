using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float backgroundSpeed;

    private Vector3 startPosition;

    private float width;

    private void Start()
    {
        startPosition = transform.position;

        width = GetComponent<MeshRenderer>().bounds.size.x / 2;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * backgroundSpeed * Time.deltaTime);

        if (transform.position.x < startPosition.x - width)
        {
            transform.position = startPosition;
        }
    }
}