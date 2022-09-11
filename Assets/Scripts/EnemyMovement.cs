using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;

    private float border = 4;
    private bool moveUp;
    
    public Rigidbody enemy;   

    public ParticleSystem fire;
    public ParticleSystem hitSmoke;
    public ParticleSystem playerFire;

    public GameObject enemyBomb;

    private bool isDeath;   

    void FixedUpdate()
    {
        BorderCheck();
        if (moveUp == true && isDeath == false)
        {
            transform.Translate(Vector3.up * Time.deltaTime * enemySpeed);
        }

        if (moveUp == false && isDeath == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * enemySpeed);
        }

        if (isDeath == true)
        {           
            transform.Rotate(Vector3.back * Time.deltaTime * 25);
            transform.Rotate(Vector3.right * Time.deltaTime * 50);
            transform.Translate(Vector3.forward * Time.deltaTime * 2);
            StartCoroutine(gravityCalc());
        }        
    }    

    IEnumerator gravityCalc() 
    {
        yield return new WaitForSeconds(2);
        enemy.useGravity = true;
    }
    IEnumerator hitPlaneDestroy()
    {
        yield return new WaitForSeconds(2);
        enemy.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            hitSmoke.Play();
            playerFire.Play();
            StartCoroutine(hitPlaneDestroy());
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        Destroy(other.gameObject);
        isDeath = true;
        fire.Play();         
    }

    private void BorderCheck() 
    {
        if (transform.position.y > border) 
        {
            moveUp = false;
        }
        if (transform.position.y < -border)
        {
            moveUp = true;
        }
    }
}
