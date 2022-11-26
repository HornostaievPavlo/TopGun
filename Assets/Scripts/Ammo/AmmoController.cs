using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public AmmoType type;

    public float ammoSpeed;

    public int RIGHT_BOUNDARY = 9;

    private const int LEFT_BOUNDARY = -9;

    private void Update()
    {
        MoveAmmoItem();
    }

    public void MoveAmmoItem()
    {
        transform.Translate(Vector3.right * ammoSpeed * Time.deltaTime);

        DestroyAmmoOutOfScreen();
    }

    private void DestroyAmmoOutOfScreen()
    {
        if (transform.position.x > RIGHT_BOUNDARY || transform.position.x < LEFT_BOUNDARY)
        {
            Destroy(gameObject);
        }
    }
}