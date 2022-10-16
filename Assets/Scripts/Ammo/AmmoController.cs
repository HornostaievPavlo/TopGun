using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] private float _ammoSpeed;

    public AmmoType _type;

    private int _rightBoundary = 9;

    private int _leftBoundary = -9;

    private void Update()
    {
        transform.Translate(Vector3.right * _ammoSpeed * Time.deltaTime);

        if (transform.position.x > _rightBoundary || transform.position.x < _leftBoundary)
        {
            Destroy(gameObject);
        }
    }
}