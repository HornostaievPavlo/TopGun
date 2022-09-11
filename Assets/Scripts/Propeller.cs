using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Vector3.left * rotateSpeed);
        if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.left * rotateSpeed * 2);
        }
    }
}
