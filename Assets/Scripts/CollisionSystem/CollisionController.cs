using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;
    //[SerializeField] private ParticleSystem playerFire;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player")) // v eb
    //    {
    //        hitSmoke.Play();
    //        playerFire.Play();

    //        FindObjectOfType<GameManager>().EndGame();
    //    }
    //}   

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("baza");
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("baza");
        Destroy(other.gameObject);
    }
}