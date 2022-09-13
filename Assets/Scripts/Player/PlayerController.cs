using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float speed;

    private CharacterController characterController;

    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        MovePlayer(moveDirection);

        FitScreenBorders();

        Fire();
    }

    private void MovePlayer(Vector3 direction)
    {
        characterController.Move(direction * speed * Time.deltaTime);
    }

    private void FitScreenBorders()
    {
        float verticalBorder = 4.0f;
        float horizontalBorder = 8.0f;

        if (transform.position.y > verticalBorder)
        {
            transform.position = new Vector3(transform.position.x, verticalBorder, transform.position.z);
        }
        if (transform.position.y < -verticalBorder)
        {
            transform.position = new Vector3(transform.position.x, -verticalBorder, transform.position.z);
        }
        if (transform.position.x > horizontalBorder)
        {
            transform.position = new Vector3(horizontalBorder, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -horizontalBorder)
        {
            transform.position = new Vector3(-horizontalBorder, transform.position.y, transform.position.z);
        }
    }

    private void Fire() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        }
    }
}