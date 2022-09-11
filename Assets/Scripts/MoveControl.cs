using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    private Vector3 start;

    private float widthCalculation;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        widthCalculation = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (transform.position.x < start.x - widthCalculation)
        {
            transform.position = start;
        }
    }
}
