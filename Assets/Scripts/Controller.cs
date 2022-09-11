using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody player;

    public GameObject bombPrefab;
    public GameObject bulletPrefab;

    public float HorFlySpeed;
    public float VerFlySpeed;

    private float leftBorder = -18.0f;
    private float rightBorder = -3.0f;
    private float upBorder = 4.0f;
    private float downBorder = -4.0f;

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement();
    }
    private void movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * horizontalMovement * HorFlySpeed);
        transform.Translate(Vector3.up * Time.deltaTime * verticalMovement * VerFlySpeed);

        if (transform.position.y > upBorder)
        {
            transform.position = new Vector3(transform.position.x, upBorder, transform.position.z);
        }
        if (transform.position.y < downBorder)
        {
            transform.position = new Vector3(transform.position.x, downBorder, transform.position.z);
        }
        if (transform.position.x < leftBorder)
        {
            transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightBorder)
        {
            transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
        }


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
