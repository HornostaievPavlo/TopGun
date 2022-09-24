using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] private float ammoSpeed;

    public AmmoType type;

    private int boundary = 9;
    
    void Update()
    {
        transform.Translate(Vector3.right * ammoSpeed * Time.deltaTime);

        if (transform.position.x > boundary) 
        {
            Destroy(gameObject);
        }
    }
}