using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;
    //[SerializeField] private ParticleSystem playerFire;

    [SerializeField]
    [Range(0, 10)] private float fallSpeed;

    private EnemyController _enemyController;

    public bool isDeath;

    private void Start()
    {
        isDeath = false;

        _enemyController = GetComponent<EnemyController>();
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("baza");
    //}
    private void Update()
    {
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("baza");
        Destroy(other.gameObject);

        fire.Play();

        _enemyController.enabled = false;

        isDeath = true;
    }

    private void UpdateState()
    {
        const float lowerBorder = -7;

        if (isDeath) transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);

        if (transform.position.y < lowerBorder) Destroy(this.gameObject);
    }
}