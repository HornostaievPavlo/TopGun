using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    //[SerializeField]
    //[Range(1, 10)] private float horizontalMovementSpeed;

    [SerializeField]
    [Range(1, 10)] private float verticalMovementSpeed;

    public BulletExplosion _bulletExplosion;

    private bool isMovingUp;

    public bool isDeath;

    public int health;

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

            Destroy(gameObject.GetComponent<Collider>());


            _bulletExplosion.ExplodePlane(transform.position);

            //StartCoroutine(IEnemyDestruction());
        }
    }

    private void CalculateHealth()
    {
        if(health <= 0) isDeath = true;
    }

    private IEnumerator IEnemyDestruction()
    {
        yield return new WaitForSeconds(5);

        Debug.Log("nahui");
        Destroy(gameObject);
    }
}