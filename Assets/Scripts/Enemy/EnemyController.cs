using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(1, 10)] private float verticalMovementSpeed;

    private bool isMovingUp;

    public bool isDeath;

    //public int health;

    private void Start()
    {
        isDeath = false;

    }

    private void Update()
    {
        Move();

        CalculateHealth();
    }

    private void Move()
    {
        if (!isDeath)
        {
            //int movingRestrictionPoint = Screen.height / 270;

            //if (transform.position.y > movingRestrictionPoint)
            //{
            //    isMovingUp = false;
            //}
            //if (transform.position.y < -movingRestrictionPoint)
            //{
            //    isMovingUp = true;
            //}

            //if (!isMovingUp)
            //{
            //    transform.Translate(Vector3.down * Time.deltaTime * verticalMovementSpeed);
            //}
            //else
            //{
            //    transform.Translate(Vector3.up * Time.deltaTime * verticalMovementSpeed);
            //}
        }
        else
        {
            Debug.LogError("death fucker");

            // test falling
            //transform.Translate(Vector3.down * Time.deltaTime * 10);

            float lowerBorder = Screen.width / -154;

            if (transform.position.y < lowerBorder)
            {
                //Destroy(gameObject);
            }
        }
    }

    private void CalculateHealth()
    {
        //if(health <= 0) isDeath = true;
    }
}