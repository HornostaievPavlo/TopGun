using UnityEngine;

public class TorqueTest : MonoBehaviour
{
    Rigidbody rb;

    public Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("TORQUE");

            rb.AddRelativeTorque(direction);
        }
    }
}