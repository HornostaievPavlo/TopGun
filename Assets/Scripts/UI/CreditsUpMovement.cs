using UnityEngine;

public class CreditsUpMovement : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 200);
    }
}
