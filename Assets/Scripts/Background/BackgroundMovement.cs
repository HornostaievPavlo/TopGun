using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private float startPosition;
    private Camera _camera;



    void Start()
    {
        _camera = Camera.main;

        startPosition = transform.position.x;
    }

    void Update()
    {
        float distance = _camera.transform.position.x * movementSpeed;

        Vector3 movementVector = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        transform.position = movementVector;
    }
}
