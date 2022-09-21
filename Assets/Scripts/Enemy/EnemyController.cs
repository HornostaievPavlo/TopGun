using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;
    //[SerializeField] private ParticleSystem playerFire;

    //public float horizontalMovementSpeed;
    public float verticalMovementSpeed;

    public bool isMovingUp;

    private void Update()
    {
        Move();

        //if (isDeath == true)
        //{
        //    transform.Rotate(Vector3.back * Time.deltaTime * 25);
        //    transform.Rotate(Vector3.right * Time.deltaTime * 50);
        //    transform.Translate(Vector3.forward * Time.deltaTime * 2);
        //    StartCoroutine(EnableGravity());
        //}
    }
    private void Move()
    {
        const float border = 4;

        if (this.transform.position.y > border)
        {
            isMovingUp = false;
        }
        if (this.transform.position.y < -border)
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

    IEnumerator EnableGravity()
    {
        yield return new WaitForSeconds(2);

        GetComponent<Rigidbody>().useGravity = true;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player")) // v eb
    //    {
    //        hitSmoke.Play();
    //        playerFire.Play();

    //        FindObjectOfType<GameManager>().EndGame();
    //    }
    //}
}