using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(1, 10)] private float verticalMovementSpeed;

    private bool isMovingUp;

    private void Update()
    {
        //Move();
    }

    private void Move()
    {
        int movingRestrictionPoint = Screen.height / 270;

        if (transform.position.y > movingRestrictionPoint)
        {
            isMovingUp = false;
        }
        if (transform.position.y < -movingRestrictionPoint)
        {
            isMovingUp = true;
        }

        if (!isMovingUp)
        {
            transform.Translate(Vector3.down * Time.deltaTime * verticalMovementSpeed);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * verticalMovementSpeed);
        }
    }
}